#define _CRT_SECURE_NO_WARNINGS

#include <Winsock.h>
#include <iostream>
#include <vector>
#include <queue>
#include <sstream>
#include <mutex>

#include <windows.h>
#include "Tokens.h"

#include <mysql_connection.h>
#include <cppconn/driver.h>
#include <cppconn/exception.h>
#include <cppconn/prepared_statement.h>

#pragma comment (lib, "ws2_32.lib")
#pragma comment (lib, "mysqlcppconn.lib")

#define MAX_ROOM_SIZE 200

std::mutex mClient;
std::mutex mRoom;

class Room;
class Client;

SOCKET serverSocket;
std::vector<Client*> connections;
std::vector<Room*> rooms;

class Client {
private:
	std::string clientID;
	std::string where;

	bool ready;
	bool player;
	int roomID;
	Room* myroom;
	SOCKET clientSocket;
public:
	Client(SOCKET clientSocket) {
		this->roomID = -1;
		this->clientSocket = clientSocket;
		where = "Lobby";
		this->ready = false;
	}
	std::string getClientID() {
		return clientID;
	}
	bool setClientID(std::string client, std::vector<Client*> connections) {
		mClient.lock();
		for (int i = 0; i < connections.size(); i++) {
			if (client == connections[i]->getClientID()) {
				mClient.unlock();
				return false;
			}
		}
		mClient.unlock();
		this->clientID = client;
		return true;
	}
	int getRoomID() {
		return roomID;
	}
	bool getPlayer() {
		return player;
	}

	void setPlayer(bool player) {
		this->player = player;
	}

	bool getReady() {
		return ready;
	}

	void setReady(bool ready) {
		this->ready = ready;
	}

	void setMyRoom(Room* room) {
		this->myroom = room;
	}

	Room* getMyRoom() {
		return this->myroom;
	}

	void setRoomID(int roomID) {
		this->roomID = roomID;
	}
	SOCKET getClientSocket() {
		return clientSocket;
	}
	std::string getWhere() {
		return where;
	}
	void setWhere(std::string where) {
		this->where = where;
	}
};

int roomNum = 0;
class Room {
private:
	int roomID;
	std::vector<Client*> people;
	std::vector<Client*> observer;
	std::string roomName;
	std::vector<std::pair<std::string, std::pair<int, int>>>board;
public:
	Room(std::string roomName, Client* user) {
		people.push_back(user);
		this->roomName = roomName;
		this->roomID = roomNum++;
	}
	int getRoomID() {
		return roomID;
	}
	std::string getRoomName() {
		return roomName;
	}
	int getPeopleNum() {
		return people.size();
	}
	int getObserverNum() {
		return observer.size();
	}

	std::vector<Client*> getObserver() {
		return observer;
	}

	std::vector<Client*> getPeople() {
		return people;
	}
	void setPeople(std::vector<Client*> people) {
		this->people = people;
	}
	bool joinRoom(Client* user) {
		if (people.size() < 2) {
			people.push_back(user);
			return true;
		}
		else {
			observer.push_back(user);
			return true;
		}
		return false;
	}
	void deleteUser(Client* user) {
		for (int i = 0; i < people.size(); i++) {
			if (people[i]->getClientID() == user->getClientID()) {
				people.erase(people.begin() + i);
				break;
			}
		}
	}

	void setBoard(int x, int y, std::string c) {
		board.push_back(std::make_pair(c, std::make_pair(x, y)));
	}

	void refreshBoard() {
		board.clear();
	}

	int getBoardSize() {
		return board.size();
	}

	std::vector<std::pair<std::string, std::pair<int, int>>> getBoard() {
		return board;
	}
};

WSAData wsaData;
SOCKADDR_IN serverAddress;

int clientCountInRoom(int roomID) {
	int count = 0;
	for (int i = 0; i < connections.size(); i++) {
		if (connections[i]->getRoomID() == roomID) {
			count++;
		}
	}
	return count;
}

void playClient(int roomID) {
	char* sent = new char[256];
	bool black = true;
	for (int i = 0; i < connections.size(); i++) {
		if (connections[i]->getRoomID() == roomID) {
			ZeroMemory(sent, 256);
			if (black) {
				sprintf(sent, "%s", "[Play]Black");
				black = false;
			}
			else {
				sprintf(sent, "%s", "[Play]White");
			}
			send(connections[i]->getClientSocket(), sent, 256, 0);
		}
	}
}

void exitClient(int roomID) {
	char* sent = new char[256];
	for (int i = 0; i < connections.size(); i++) {
		if (connections[i]->getRoomID() == roomID) {
			ZeroMemory(sent, 256);
			sprintf(sent, "%s", "[Exit]");
			send(connections[i]->getClientSocket(), sent, 256, 0);
		}
	}
}

void putClient(Client* client, int x, int y, std::string c) {
	char* sent = new char[256];
	Room* room = client->getMyRoom();
	client->getMyRoom()->setBoard(x, y, c);

	mRoom.lock();
	for (int i = 0; i < room->getPeopleNum(); i++) {
		std::string data = "[Put]" + std::to_string(x) + "," + std::to_string(y);
		send(room->getPeople()[i]->getClientSocket(), data.c_str(), data.length(), 0);
	}

	for (int i = 0; i < room->getObserverNum(); i++) {
		std::string data = "[Put]";
		for (int j = 0; j < room->getBoardSize(); j++) {
			data += room->getBoard()[j].first + "," + std::to_string(room->getBoard()[i].second.first) +
				"," + std::to_string(room->getBoard()[i].second.second) + ";";
		}
		send(room->getObserver()[i]->getClientSocket(), data.c_str(), data.length(), 0);
	}
	mRoom.unlock();
}

void refreshUser() {
	mClient.lock();
	std::string conn = "[People]";
	for (int i = 0; i < connections.size(); i++) {
		if (connections[i]->getWhere() == "Lobby") {
			conn.append(connections[i]->getClientID());
			conn.append(",");
		}
	}
	for (int i = 0; i < connections.size(); i++) {
		send(connections[i]->getClientSocket(), conn.c_str(), conn.length() - 1, 0);
	}
	mClient.unlock();
}
void refreshRoom() {
	mRoom.lock();
	std::string conn = "[Room]";
	for (int i = 0; i < rooms.size(); i++) {
		conn.append(std::to_string(rooms[i]->getRoomID()));
		conn.append(",");
		conn.append(rooms[i]->getRoomName());
		conn.append(",");
		conn.append(std::to_string(rooms[i]->getPeopleNum()));
		conn.append(";");
	}
	mRoom.unlock();

	mClient.lock();
	for (int i = 0; i < connections.size(); i++) {
		if (connections[i]->getWhere() == "Lobby") {
			send(connections[i]->getClientSocket(), conn.c_str(), conn.length(), 0);
		}
	}
	mClient.unlock();
}

const sql::SQLString server = "tcp://127.0.0.1:3306";
const sql::SQLString username = "root";
const sql::SQLString password = "mysql_p@ssw0rd";

void ServerThread(Client* client) {
	char* sent = new char[256];
	char* received = new char[256];
	int size = 0;

	sql::Connection* con;
	sql::Driver* driver;
	sql::PreparedStatement* pstmt;
	sql::Statement* stmt;
	sql::ResultSet* result;

	//로그인
	while (true) {
		ZeroMemory(received, 256);
		if ((size = recv(client->getClientSocket(), received, 256, NULL)) > 0) {
			std::string receivedString = std::string(received);
			getTokens(receivedString, ']');
			try {
				driver = get_driver_instance();
				con = driver->connect(server, username, password);
			}
			catch (sql::SQLException e) {
				std::cout << e.what() << std::endl;
				return;
			}

			con->setSchema("omok");


			if (receivedString.find("[Login]") != -1) {
				getTokens(temp[1], ',');
				std::string id = temp[0];
				std::string password = temp[1];

				pstmt = con->prepareStatement("SELECT * FROM member WHERE id=? AND password=?");
				pstmt->setString(1, id.c_str());
				pstmt->setString(2, password.c_str());
				result = pstmt->executeQuery();

				if (result->next()) {
					bool loginChk = client->setClientID(id, connections);
					if (loginChk == false) {
						send(client->getClientSocket(), "already", 7, NULL);
					}
					else {
						mClient.lock();
						connections.push_back(client);
						mClient.unlock();

						send(client->getClientSocket(), "valid", 5, NULL);
						break;
					}
				}
				else {
					send(client->getClientSocket(), "invalid", 7, NULL);
				}
			}
		}
	}

	if (client->getClientID() == "") return;
	std::cout << "[ 새로운 사용자 접속 ] " << client->getClientID() << std::endl;

	//데이터처리
	while (true) {
		ZeroMemory(received, 256);
		if ((size = recv(client->getClientSocket(), received, 256, NULL)) > 0) {
			std::string receivedString = std::string(received);
			getTokens(receivedString, ']');

			
			if (receivedString.find("[Room]") != -1) {
				
				getTokens(temp[1], ',');
								
				if (temp[0] == "Join") {
					std::cout << client->getClientID() << "님이 접속 하였습니다." << std::endl;
					std::string msg = "[Join]";
					mRoom.lock();
					for (int i = 0; i < rooms.size(); i++) {
						if (std::to_string(rooms[i]->getRoomID()) == temp[1].c_str()) {
							if (rooms[i]->joinRoom(client)) {
								client->setWhere("Room");
								client->setRoomID(rooms[i]->getRoomID());
								client->setMyRoom(rooms[i]);

								refreshUser();
								Sleep(10);
								//msg.append("succ");
								//send(client->getClientSocket(), msg.c_str(), msg.length(), 0);

								Sleep(100);
								if (client->getPlayer() == true) {
									msg += ("succ,Player," + std::to_string(rooms[i]->getRoomID()));
								}
								else {
									msg += ("succ,Observer," + std::to_string(rooms[i]->getRoomID()));
								}
								
								send(client->getClientSocket(), msg.c_str(), msg.length(), 0);
								Sleep(100);
								send(client->getClientSocket(), " ", 1, 0);
							}
							else {
								msg.append("fail");
								send(client->getClientSocket(), msg.c_str(), msg.length(), 0);
							}
							break;
						}
					}
					mRoom.unlock();
				}
				else if (temp[0] == "Exit") {
					mRoom.lock();
					for (int i = 0; i < rooms.size(); i++) {
						if (rooms[i]->getRoomID() == atoi(temp[1].c_str())) {
							client->setWhere("Lobby");
							rooms[i]->deleteUser(client);

							if (rooms[i]->getPeopleNum() == 0) {
								rooms.erase(rooms.begin() + i);
							}
							break;
						}
					}
					mRoom.unlock();
					mRoom.lock();
					for (int i = 0; i < rooms.size(); i++) {
						if (rooms[i]->getRoomID() == client->getRoomID()) {
							client->setWhere("Lobby");
							rooms[i]->deleteUser(client);

							if (rooms[i]->getPeopleNum() == 0) {
								rooms.erase(rooms.begin() + i);
							}
							break;
						}
					}
					mRoom.unlock();

					refreshUser();
					Sleep(10);
					refreshRoom();
				}
			}
			/*else if (receivedString.find("[Create][Room]") != -1) {
				client->setWhere("room");
				Room* room = new Room(tokens[2], client);
				rooms.push_back(room);
				refreshUser();
				Sleep(10);
				std::string conn = "[Create]" + std::to_string(room->getRoomID());
				send(client->getClientSocket(), conn.c_str(), conn.length(), 0);
			}
			else if (receivedString.find("[Refresh][Room]") != -1) {
				refreshRoom();
			}*/
			std::string t;
			if (receivedString.find("[Enter]") != -1) {
				/* 메시지를 보낸 클라이언트를 찾기 */
				for (int i = 0; i < connections.size(); i++){
					getTokens(temp[1], ',');
					std::string roomID = temp[0];
					std::string UserID = temp[1];
					t = temp[2];
					std::cout << temp[1] << "님이  " << temp[0] << "에 접속하였습니다. " << std::endl;
					//connections[i].getID = UserID;
					int roomInt = atoi(roomID.c_str());
					int clientCount = 0;
					if (connections[i]->getClientSocket() == client->getClientSocket()) {
						if (t == "0") {
							clientCount = clientCountInRoom(roomInt);
							std::cout << clientCount;
						}
						/* 2명 이상이 동일한 방에 들어가 있는 경우 가득 찼다고 전송 */
						if (clientCount >= 2 && t == "0") {
							ZeroMemory(sent, 256);
							sprintf(sent, "%s", "[Full]");
							send(connections[i]->getClientSocket(), sent, 256, 0);
							break;
						}
						std::cout << "클라이언트 [" << client->getClientID() << "] " << UserID << "님이 " << roomID << "번 방으로 접속" << std::endl;
						/* 해당 사용자의 방 접속 정보 갱신 */
						Client* newClient = new Client(*client);
						newClient->setRoomID(roomInt);
						connections[i] = newClient;
						/* 방에 성공적으로 접속했다고 메시지 전송 */
						ZeroMemory(sent, 256);
						sprintf(sent, "%s", "[Enter]");
						send(connections[i]->getClientSocket(), sent, 256, 0);
						/* 상대방이 이미 방에 들어가 있는 경우 게임 시작 */
						if (clientCount == 1) {
							playClient(roomInt);
						}
					}
				}
			}
			else if (receivedString.find("[Put]") != -1 || t == "1") {
				/* 메시지를 보낸 클라이언트 정보 받기 */
				std::string data = temp[1];
				getTokens(data, ',');
				int roomID = atoi(temp[0].c_str());
				int x = atoi(temp[1].c_str());
				int y = atoi(temp[2].c_str());
				std::string c = temp[2].c_str();
				/* 사용자가 놓은 돌의 위치를 전송 */
				putClient(client, x, y,c);
			}
			else if (receivedString.find("[Play]") != -1) {
				/* 메시지를 보낸 클라이언트를 찾기 */
				std::string roomID = temp[1];
				int roomInt = atoi(roomID.c_str());
				/* 사용자가 놓은 돌의 위치를 전송 */
				playClient(roomInt);
			}
			else if (receivedString.find("[Observer]") != -1) {
				Room* room = client->getMyRoom();
				std::string data = "[Put]";
				for (int i = 0; i < room->getBoardSize(); i++) {
					//data += std::to_string(room->getBoard()[i].first) + "," + std::to_string(room->getBoard()[i].second) + ";";
					data += room->getBoard()[i].first + "," + std::to_string(room->getBoard()[i].second.first) + "," + std::to_string(room->getBoard()[i].second.second) + ";";
				}
				send(client->getClientSocket(), data.c_str(), data.length(), 0);
			}

		}
		else {
			ZeroMemory(sent, 256);
			sprintf(sent, "클라이언트 [%s]의 연결이 끊어졌습니다.", client->getClientID().c_str());
			std::cout << sent << std::endl;
			/* 게임에서 나간 플레이어를 찾기 */
			mClient.lock();
			for (int i = 0; i < connections.size(); i++) {
				if (connections[i]->getClientID() == client->getClientID()) {
					/* 다른 사용자와 게임 중이던 사람이 나간 경우 */
					if (connections[i]->getRoomID() != -1 &&
						clientCountInRoom(connections[i]->getRoomID()) == 2) {
						/* 남아있는 사람에게 메시지 전송 */
						exitClient(connections[i]->getRoomID());
					}
					connections.erase(connections.begin() + i);

					std::string conn = "[People]";
					for (int i = 0; i < connections.size(); i++) {
						conn.append(connections[i]->getClientID());
						conn.append(",");
					}
					//conn.erase(conn.end());

					for (int i = 0; i < connections.size(); i++) {
						send(connections[i]->getClientSocket(), conn.c_str(), conn.length() - 1, 0);
					}
					break;
				}
			}
			mClient.unlock();
			delete client;
			//std::cout << serverAddress.sin_addr.S_un.S_addr<<"종료" << std::endl;
			con->close();
			break;
		}
	}
}

int main() {
	WSAStartup(MAKEWORD(2, 2), &wsaData);
	serverSocket = socket(AF_INET, SOCK_STREAM, NULL);

	serverAddress.sin_addr.s_addr = inet_addr("127.0.0.1");
	serverAddress.sin_port = htons(9876);
	serverAddress.sin_family = AF_INET;

	std::cout << "[ C++ 오목 게임 서버 가동 ]" << std::endl;
	bind(serverSocket, (SOCKADDR*)&serverAddress, sizeof(serverAddress));
	listen(serverSocket, 32);

	int addressLength = sizeof(serverAddress);
	while (true) {
		SOCKET clientSocket = socket(AF_INET, SOCK_STREAM, NULL);
		if (clientSocket = accept(serverSocket, (SOCKADDR*)&serverAddress, &addressLength)) {
			//std::cout << serverAddress.sin_addr.S_un.S_addr <<"연결"<< std::endl;
			Client* client = new Client(clientSocket);
			CreateThread(NULL, NULL, (LPTHREAD_START_ROUTINE)ServerThread, (LPVOID)client, NULL, NULL);
		}
		Sleep(100);
	}
}