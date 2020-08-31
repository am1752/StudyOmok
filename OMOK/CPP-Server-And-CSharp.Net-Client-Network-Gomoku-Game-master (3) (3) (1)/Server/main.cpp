#define _CRT_SECURE_NO_WARNINGS

#include <Winsock.h>
#include <iostream>
#include <vector>
#include <queue>
#include <sstream>
#include <mutex>

#include <windows.h>

#include <mysql_connection.h>
#include <cppconn/driver.h>
#include <cppconn/exception.h>
#include <cppconn/prepared_statement.h>

#include "Client.h"
#include "Room.h"

#pragma comment (lib, "ws2_32.lib")
#pragma comment (lib, "mysqlcppconn.lib")

#define MAX_ROOM_SIZE 200
#define RECV_BUF_SIZE 256

SOCKET serverSocket;
std::vector<Client*> connections;
std::vector<Room*> rooms;

WSAData wsaData;
SOCKADDR_IN serverAddress;

std::mutex mClient;
std::mutex mRoom;

std::vector<std::string> getTokens(std::string input, char delimiter) {
	std::vector<std::string> tokens;
	std::istringstream f(input);
	std::string s;
	while (std::getline(f, s, delimiter)) {
		tokens.push_back(s);
	}
	return tokens;
}

int clientCountInRoom(int roomID) {
	int count = 0;
	for (int i = 0; i < connections.size(); i++) {
		if (connections[i]->getRoomID() == roomID) {
			count++;
		}
	}
	return count;
}

//void playClient(int roomID) {
//	char* sent = new char[RECV_BUF_SIZE];
//	bool black = true;
//	for (int i = 0; i < connections.size(); i++) {
//		if (connections[i]->getRoomID() == roomID) {
//			ZeroMemory(sent, RECV_BUF_SIZE);
//			if (black) {
//				sprintf(sent, "%s", "[Play]Black");
//				black = false;
//			}
//			else {
//				sprintf(sent, "%s", "[Play]White");
//			}
//			send(connections[i]->getClientSocket(), sent, RECV_BUF_SIZE, 0);
//		}
//	}
//}

void exitClient(int roomID) {
	char* sent = new char[RECV_BUF_SIZE];
	for (int i = 0; i < connections.size(); i++) {
		if (connections[i]->getRoomID() == roomID) {
			ZeroMemory(sent, RECV_BUF_SIZE);
			sprintf(sent, "%s", "[Exit]");
			send(connections[i]->getClientSocket(), sent, RECV_BUF_SIZE, 0);
		}
	}
}

void putClient(Client* client, int x, int y, std::string c) {
	Room* room = client->getMyRoom();
	client->getMyRoom()->setBoard(x, y, c);

	mRoom.lock();
	for (int i = 0; i < room->getPeopleNum(); i++) {
		std::string msg = "[Put]" + std::to_string(x) + "," + std::to_string(y);
		send(room->getPeople()[i]->getClientSocket(), msg.c_str(), msg.length(), 0);
	}

	for (int i = 0; i < room->getObserverNum(); i++) {
		std::string msg = "[Put]";
		for (int i = 0; i < room->getBoardSize(); i++) {
			msg += room->getBoard()[i].first + "," + std::to_string(room->getBoard()[i].second.first) + "," + std::to_string(room->getBoard()[i].second.second) + ";";
		}
		send(room->getObserver()[i]->getClientSocket(), msg.c_str(), msg.length(), 0);
	}
	mRoom.unlock();
}

void refreshUser(Client* client) {
	mClient.lock();
	std::string msg = "[People]";
	for (int i = 0; i < connections.size(); i++) {
		if (connections[i]->getLocation() == "Lobby") {
			msg.append(connections[i]->getClientID());
			msg.append(",");
		}
	}
	for (int i = 0; i < connections.size(); i++) {
		send(connections[i]->getClientSocket(), msg.c_str(), msg.length() - 1, 0);
	}
	mClient.unlock();
}
void refreshRoom(Client* client) {
	mRoom.lock();
	std::string msg = "[Room]";
	for (int i = 0; i < rooms.size(); i++) {
		msg.append(std::to_string(rooms[i]->getRoomID()));
		msg.append(",");
		msg.append(rooms[i]->getRoomName());
		msg.append(",");
		msg.append(std::to_string(rooms[i]->getPeopleNum()));
		msg.append(",");
		msg.append(std::to_string(rooms[i]->getObserverNum()));
		msg.append(";");
	}
	mRoom.unlock();

	mClient.lock();
	for (int i = 0; i < connections.size(); i++) {
		if (connections[i]->getLocation() == "Lobby") {
			send(connections[i]->getClientSocket(), msg.c_str(), msg.length(), 0);
		}
	}
	mClient.unlock();
}

void refreshLobby(Client* client) {
	std::string msg = "[Refresh]";
	mRoom.lock();
	if (rooms.size() == 0) {
		msg.append("empty;");
	}
	else {
		for (int i = 0; i < rooms.size(); i++) {
			msg.append(std::to_string(rooms[i]->getRoomID()));
			msg.append(",");
			msg.append(rooms[i]->getRoomName());
			msg.append(",");
			msg.append(std::to_string(rooms[i]->getPeopleNum()));
			msg.append(",");
			msg.append(std::to_string(rooms[i]->getObserverNum()));
			msg.append(";");
		}
	}
	msg.append("/");
	mRoom.unlock();

	mClient.lock();
	for (int i = 0; i < connections.size(); i++) {
		if (connections[i]->getLocation() == "Lobby") {
			msg.append(connections[i]->getClientID());
			msg.append(",");
		}
	}
	for (int i = 0; i < connections.size(); i++) {
		send(connections[i]->getClientSocket(), msg.c_str(), msg.length() - 1, 0);
	}
	mClient.unlock();
}

const sql::SQLString server = "tcp://127.0.0.1:3306";
const sql::SQLString username = "root";
const sql::SQLString password = "mysql_p@ssw0rd";

void ServerThread(Client* client) {
	char* sent = new char[RECV_BUF_SIZE];
	char* received = new char[256];
	int size = 0;

	sql::Connection* con;
	sql::Driver* driver;
	sql::PreparedStatement* pstmt;
	sql::Statement* stmt;
	sql::ResultSet* result;
	
	client->setMutex(&mClient);
	//로그인
	while (true) {
		ZeroMemory(received, RECV_BUF_SIZE);
		if ((size = recv(client->getClientSocket(), received, RECV_BUF_SIZE, NULL)) > 0) {
			std::string receivedString = std::string(received);
			std::vector<std::string> tokens = getTokens(receivedString, ']');
			try {
				driver = get_driver_instance();
				con = driver->connect(server, username, password);
			}
			catch (sql::SQLException e) {
				std::cout << e.what() << std::endl;
				return;
			}

			con->setSchema("gomoku");


			if (receivedString.find("[Login]") != -1) {
				std::vector<std::string> dataTokens = getTokens(tokens[1], ',');
				std::string id = dataTokens[0];
				std::string password = dataTokens[1];

				pstmt = con->prepareStatement("SELECT * FROM membertbl WHERE id=? AND password=?");
				pstmt->setString(1, id.c_str());
				pstmt->setString(2, password.c_str());
				result = pstmt->executeQuery();

				if (result->next()) {
					
					bool loginChk = client->setClientID(id, connections);
					if (loginChk == false) {
						send(client->getClientSocket(), "already", 7, 0);
					}
					else {
						mClient.lock();
						connections.push_back(client);
						mClient.unlock();
						
						send(client->getClientSocket(), "valid", 5, 0);
						break;
					}
				}
				else {
					send(client->getClientSocket(), "invalid", 7, 0);
				}
			}
			else if (receivedString.find("[Sign]") != -1) {
				std::vector<std::string> dataTokens = getTokens(tokens[1], ',');
				std::string id = dataTokens[0];
				std::string password = dataTokens[1];

				pstmt = con->prepareStatement("INSERT INTO membertbl(id, password) VALUES(?,?)");
				pstmt->setString(1, id.c_str());
				pstmt->setString(2, password.c_str());

				try {
					int r = pstmt->executeUpdate();
					send(client->getClientSocket(), "succ", 4, 0);
				}
				catch (sql::SQLException e) {
					send(client->getClientSocket(), "fail", 4, 0);
				}
			}
		}
	}

	if (client->getClientID() == "") return;
	std::cout << "[ 새로운 사용자 접속 ] " << client->getClientID() << std::endl;

	//데이터처리
	while (true) {
		ZeroMemory(received, RECV_BUF_SIZE);
		if ((size = recv(client->getClientSocket(), received, RECV_BUF_SIZE, NULL)) > 0) {
			std::string receivedString = std::string(received);
			std::vector<std::string> tokens = getTokens(receivedString, ']');

			if (receivedString.find("[Lobby]") != -1) {
				std::vector<std::string> lobbyTokens = getTokens(tokens[1], 0x01);
				if (lobbyTokens[0] == "Refresh") {
					refreshLobby(client);
				}
				else if (lobbyTokens[0] == "People") {
					refreshUser(client);
				}
				else if (lobbyTokens[0] == "Chat") {
					std::string msg = "[Chat]" + client->getClientID() + " " + lobbyTokens[1];
					
					mClient.lock();
					for (int i = 0; i < connections.size(); i++) {
						if (connections[i]->getLocation() == "Lobby")
							send(connections[i]->getClientSocket(), msg.c_str(), msg.length(), 0);
					}
					mClient.unlock();
				}
			}
			else if (receivedString.find("[Invite]") != -1) {
				std::vector<std::string> inviteTokens = getTokens(tokens[1], ',');
				bool inviteChk = false;
				for (int i = 0; i < connections.size(); i++) {
					if ((connections[i]->getClientID() == inviteTokens[0]) && (connections[i]->getLocation() == "Lobby")) {
						std::string msg = "[Invite]" + inviteTokens[1];
						send(connections[i]->getClientSocket(), msg.c_str(), msg.length(), 0);
						inviteChk = true;
						break;
					}
				}
				if (inviteChk == false) {
					std::string msg = "[Invite]fail";
					send(client->getClientSocket(), msg.c_str(), msg.length(), 0);
				}
			}
			else if (receivedString.find("[Room]") != -1) {
				std::vector<std::string> roomTokens = getTokens(tokens[1], 0x01);
				if (roomTokens[0] == "Create") {

					Room* room = new Room(roomTokens[1], client);
					rooms.push_back(room);

					client->setLocation("Room");
					client->setRoomID(room->getRoomID());
					client->setMyRoom(room);
					client->setPlayer(true);

					refreshUser(client);
					Sleep(10);
					std::string msg = "[Create]" + std::to_string(room->getRoomID());
					send(client->getClientSocket(), msg.c_str(), msg.length(), 0);
					
					Sleep(100);
					send(client->getClientSocket(), " ", 1, 0);
				}
				else if (roomTokens[0] == "Refresh") {
					refreshRoom(client);
				}
				else if (roomTokens[0] == "Join") {
					std::string msg = "[Join]";
					mRoom.lock();
					for (int i = 0; i < rooms.size(); i++) {
						if (std::to_string(rooms[i]->getRoomID()) == roomTokens[1].c_str()) {
							if (rooms[i]->joinRoom(client)) {
								client->setLocation("Room");
								client->setRoomID(rooms[i]->getRoomID());
								client->setMyRoom(rooms[i]);
								
								refreshUser(client);
								Sleep(10);

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
				else if (roomTokens[0] == "Exit") {
					mRoom.lock();
					for (int i = 0; i < rooms.size(); i++) {
						if (rooms[i]->getRoomID() == client->getRoomID()) {

							client->setLocation("Lobby");
							client->setMyRoom(NULL);
							client->setRoomID(-1);

							rooms[i]->deleteUser(client);

							if (rooms[i]->getPeopleNum() == 0) {
								rooms.erase(rooms.begin() + i);
							}
							break;
						}
					}
					mRoom.unlock();
					refreshLobby(client);
				}
				else if (roomTokens[0] == "Ready") {
					mRoom.lock();
					client->setReady(true);
					Room* myRoom = client->getMyRoom();
					bool readyChk = true;
					if (myRoom->getPeopleNum() == 2) {
						for (int i = 0; i < myRoom->getPeopleNum(); i++) {
							if (myRoom->getPeople()[i]->getReady() == false) {
								readyChk = false;
								break;
							}
						}
						if (readyChk == true) {
							std::string msg = "[Play]Black";
							send(myRoom->getPeople()[0]->getClientSocket(), msg.c_str(), msg.length(), 0);

							msg = "[Play]White";
							send(myRoom->getPeople()[1]->getClientSocket(), msg.c_str(), msg.length(), 0);
						}
					}
					mRoom.unlock();
				}
				else if (roomTokens[0] == "Chat") {
					Room* room = client->getMyRoom();
					std::string msg = "[Chat]" + client->getClientID() + " " + roomTokens[1];
					for (int i = 0; i < room->getPeopleNum(); i++) {
						send(room->getPeople()[i]->getClientSocket(), msg.c_str(), msg.length(), 0);
					}
					for (int i = 0; i < room->getObserverNum(); i++) {
						send(room->getObserver()[i]->getClientSocket(), msg.c_str(), msg.length(), 0);
					}
				}
			}
			else if (receivedString.find("[Put]") != -1) {
				std::vector<std::string> putTokens = getTokens(tokens[1], ',');
				int x = atoi(putTokens[0].c_str());
				int y = atoi(putTokens[1].c_str());
				std::string c = putTokens[2].c_str();
				putClient(client, x, y, c);
			}
			else if (receivedString.find("[Observer]") != -1) {
				Room* room = client->getMyRoom();
				std::string msg = "[Put]";
				for (int i = 0; i < room->getBoardSize(); i++) {
					msg += room->getBoard()[i].first + "," + std::to_string(room->getBoard()[i].second.first) + "," + std::to_string(room->getBoard()[i].second.second) + ";";
				}
				send(client->getClientSocket(), msg.c_str(), msg.length(), 0);
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

					std::string msg = "[People]";
					for (int i = 0; i < connections.size(); i++) {
						msg.append(connections[i]->getClientID());
						msg.append(",");
					}
					//conn.erase(conn.end());

					for (int i = 0; i < connections.size(); i++) {
						send(connections[i]->getClientSocket(), msg.c_str(), msg.length() - 1, 0);
					}
					break;
				}
			}
			mClient.unlock();
			delete client;
			con->close();
			break;
		}
	}
}

int main() {
	WSAStartup(MAKEWORD(2, 2), &wsaData);
	serverSocket = socket(AF_INET, SOCK_STREAM, NULL);

	serverAddress.sin_addr.s_addr = htonl(INADDR_ANY);//inet_addr();
	serverAddress.sin_port = htons(9876);
	serverAddress.sin_family = AF_INET;

	std::cout << "[ C++ 오목 게임 서버 가동 ]" << std::endl;
	bind(serverSocket, (SOCKADDR*)&serverAddress, sizeof(serverAddress));
	listen(serverSocket, 32);

	int addressLength = sizeof(serverAddress);
	while (true) {
		SOCKET clientSocket = socket(AF_INET, SOCK_STREAM, NULL);
		if (clientSocket = accept(serverSocket, (SOCKADDR*)&serverAddress, &addressLength)) {
			Client* client = new Client(clientSocket);
			CreateThread(NULL, NULL, (LPTHREAD_START_ROUTINE)ServerThread, (LPVOID)client, NULL, NULL);
		}
		Sleep(100);
	}
}