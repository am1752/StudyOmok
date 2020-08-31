#define _CRT_SECURE_NO_WARNINGS

#include <windows.h>
#include <Winsock.h>
#include <iostream>
#include <vector>
#include <sstream>
#include <mutex>

#include <mysql_connection.h>
#include <cppconn/driver.h>
#include <cppconn/exception.h>
#include <cppconn/prepared_statement.h>
#include "Tokens.h"

#pragma comment (lib, "ws2_32.lib")
#pragma comment (lib, "mysqlcppconn.lib")

//using namespace std;
std::mutex m;

class Client {
private:
	std::string clientID;
	int roomID;
	//string ID;
	SOCKET clientSocket;
public:
	Client(SOCKET clientSocket) {
		this->roomID = -1;
		this->clientSocket = clientSocket;
		//this->ID = "";
	}
	std::string getClientID() {
		return clientID;
	}

	bool setClientID(std::string client, std::vector<Client> connections) {
		m.lock();
		for (int i = 0; i < connections.size(); i++) {
			if (client == connections[i].getClientID()) {
				m.unlock();
				return false;
			}
		}
		m.unlock();
		this->clientID = client;
		return true;
	}


	int getRoomID() {
		return roomID;
	}
	void setRoomID(int roomID) {
		this->roomID = roomID;
	}
	SOCKET getClientSocket() {
		return clientSocket;
	}
};

SOCKET serverSocket;
std::vector<Client> connections;
WSAData wsaData;
SOCKADDR_IN serverAddress;

int nextID;
std::string UserID;
std::string t;

const sql::SQLString server = "tcp://127.0.0.1:3306";
const sql::SQLString username = "root";
const sql::SQLString password = "mysql_p@ssw0rd";

//std::vector<std::string> getTokens(std::string input, char delimiter) {
//	std::vector<std::string> tokens1;
//	std::istringstream f(input);
//	std::string s;
//	while (std::getline(f, s, delimiter)) {
//		tokens1.push_back(s);
//	}
//	return tokens1;
//}
//
//std::string getTokens1(std::string input, char delimiter) {
//	std::vector<std::string> tokens;
//	std::istringstream f(input);
//	std::string s;
//
//	while (getline(f, s, delimiter)) {
//		tokens.push_back(s);
//	}
//
//	return tokens[1];
//}


int clientCountInRoom(int roomID) {
	int count = 0;
	for (int i = 0; i < connections.size(); i++) {
		if (connections[i].getRoomID() == roomID) {
			//connections[i].getID = UserID;
			count++;
		}
	}
	return count;
}

void playClient(int roomID) {
	char* sent = new char[256];
	bool black = true;
	for (int i = 0; i < connections.size(); i++) {
		if (connections[i].getRoomID() == roomID) {
			ZeroMemory(sent, 256);
			if (black) {
				sprintf(sent, "%s", "[Play]Black");
				black = false;
			}
			else {
				sprintf(sent, "%s", "[Play]White");
			}
			send(connections[i].getClientSocket(), sent, 256, 0);
		}
	}
}

void exitClient(int roomID) {
	char* sent = new char[256];
	for (int i = 0; i < connections.size(); i++) {
		if (connections[i].getRoomID() == roomID) {
			ZeroMemory(sent, 256);
			sprintf(sent, "%s", "[Exit]");
			send(connections[i].getClientSocket(), sent, 256, 0);
		}
	}
}

void putClient(int roomID, int x, int y) {
	char* sent = new char[256];
	for (int i = 0; i < connections.size(); i++) {
		if (connections[i].getRoomID() == roomID) {
			ZeroMemory(sent, 256);
			std::string data = "[Put]" + std::to_string(x) + "," + std::to_string(y);
			sprintf(sent, "%s", data.c_str());
			send(connections[i].getClientSocket(), sent, 256, 0);
		}
	}
}

void ServerThread(Client* client) {
	char* sent = new char[256];
	char* received = new char[256];
	int size = 0;
	while (true) {
		ZeroMemory(received, 256);
		std::string hi;
		if ((size = recv(client->getClientSocket(), received, 256, NULL)) > 0) {
			std::string receivedString = std::string(received);
			//if (receivedString.find("[Login]") != -1) hi = getTokens1(receivedString, ']');
			//std::vector<std::string> tokens = getTokens(receivedString, ']');

			sql::Driver* driver;
			sql::Connection* con;
			sql::PreparedStatement* pstmt;
			sql::Statement* stmt;
			sql::ResultSet* result;

			try {
				driver = get_driver_instance();
				con = driver->connect(server, username, password);
			}
			catch (sql::SQLException e) {
				std::cout << e.what() << std::endl;
				return;
			}

			con->setSchema("omok");

			/*if (receivedString.find("[ID]") != -1) {
				UserID = tokens[1];
			}*/
			getTokens(receivedString, ']');
			std::vector<std::string> tokens = temp;
			if (receivedString.find("[Login]") != -1) {
				getTokens(tokens[1], ',');
				std::vector<std::string> dataTokens = temp;
				std::cout << tokens[1] << std::endl;
				std::string id = dataTokens[0];
				std::string password = dataTokens[1];

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
						m.lock();
						connections.push_back(*client);
						m.unlock();

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
	//if (client->getClientID() == "") return;
	std::cout << "[ ���ο� ����� ���� ] " << client->getClientID() << std::endl;
	

	while (true) {
		ZeroMemory(received, 256);
		if ((size = recv(client->getClientSocket(), received, 256, NULL)) > 0) {
			std::string receivedString = std::string(received);
			getTokens(receivedString, ']');
			std::vector<std::string> tokens = temp;

			sql::Driver* driver;
			sql::Connection* con;
			sql::PreparedStatement* pstmt;
			sql::Statement* stmt;
			sql::ResultSet* result;

			try {
				driver = get_driver_instance();
				con = driver->connect(server, username, password);
			}
			catch (sql::SQLException e) {
				std::cout << e.what() << std::endl;
				return;
			}


			if (receivedString.find("[ROOM]") != -1) {
				std::cout << tokens[1] <<"���� �κ� ����"<< std::endl;
			}

			if (receivedString.find("[MULTI]") != -1) {
				getTokens(tokens[1], ',');
				std::cout << tokens[1] << "���� " << tokens[0] << "�� ����";
			}

			if (receivedString.find("[ENTER_ROOM]") != -1) {
				std::cout << client->getClientID() << tokens[1] << std::endl;
			}

			if (receivedString.find("[ID]") != -1) {
				std::cout << tokens[1] << "���� �濡 ����" << std::endl;
			}

			if (receivedString.find("[Enter]") != -1) {
				/* �޽����� ���� Ŭ���̾�Ʈ�� ã�� */
				for (int i = 0; i < connections.size(); i++) {
					getTokens(tokens[1], ',');
					std::vector<std::string> Users = temp;
					std::string roomID = Users[0];
					UserID = Users[1];
					t = Users[2];
					std::cout << Users[1] << "����  " << Users[0] << "�� �����Ͽ����ϴ�. " << std::endl;
					//connections[i].getID = UserID;
					int roomInt = atoi(roomID.c_str());
					int clientCount = 0;
					if (connections[i].getClientSocket() == client->getClientSocket()) {
						if (t == "0") {
							clientCount = clientCountInRoom(roomInt);
							std::cout << clientCount;
						}
						/* 2�� �̻��� ������ �濡 �� �ִ� ��� ���� á�ٰ� ���� */
						if (clientCount >= 2 && t == "0") {
							ZeroMemory(sent, 256);
							sprintf(sent, "%s", "[Full]");
							send(connections[i].getClientSocket(), sent, 256, 0);
							break;
						}
						std::cout << "Ŭ���̾�Ʈ [" << client->getClientID() << "] " << UserID << "���� " << roomID << "�� ������ ����" << std::endl;
						/* �ش� ������� �� ���� ���� ���� */
						Client* newClient = new Client(*client);
						newClient->setRoomID(roomInt);
						connections[i] = *newClient;
						/* �濡 ���������� �����ߴٰ� �޽��� ���� */
						ZeroMemory(sent, 256);
						sprintf(sent, "%s", "[Enter]");
						send(connections[i].getClientSocket(), sent, 256, 0);
						/* ������ �̹� �濡 �� �ִ� ��� ���� ���� */
						if (clientCount == 1) {
							playClient(roomInt);
						}
					}
				}
			}
			else if (receivedString.find("[Put]") != -1 || t == "1") {
				/* �޽����� ���� Ŭ���̾�Ʈ ���� �ޱ� */
				std::string data = tokens[1];
				getTokens(data, ',');
				std::vector<std::string> dataTokens = temp;
				int roomID = atoi(dataTokens[0].c_str());
				int x = atoi(dataTokens[1].c_str());
				int y = atoi(dataTokens[2].c_str());
				/* ����ڰ� ���� ���� ��ġ�� ���� */
				putClient(roomID, x, y);
			}
			else if (receivedString.find("[Play]") != -1) {
				/* �޽����� ���� Ŭ���̾�Ʈ�� ã�� */
				std::string roomID = tokens[1];
				int roomInt = atoi(roomID.c_str());
				/* ����ڰ� ���� ���� ��ġ�� ���� */
				playClient(roomInt);
			}

		}
		else{
			ZeroMemory(sent, 256);
			sprintf(sent, "Ŭ���̾�Ʈ [%s]�� ������ ���������ϴ�.", client->getClientID().c_str());
			std::cout << sent << std::endl;
			/* ���ӿ��� ���� �÷��̾ ã�� */
			m.lock();
			for (int i = 0; i < connections.size(); i++) {
				if (connections[i].getClientID() == client->getClientID()) {
					/* �ٸ� ����ڿ� ���� ���̴� ����� ���� ��� */
					if (connections[i].getRoomID() != -1 &&
						clientCountInRoom(connections[i].getRoomID()) == 2) {
						/* �����ִ� ������� �޽��� ���� */
						exitClient(connections[i].getRoomID());
					}
					connections.erase(connections.begin() + i);
					break;
				}
			}
			m.unlock();
			delete client;
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

	std::cout << "[ C++ ���� ���� ���� ���� ]" << std::endl;
	bind(serverSocket, (SOCKADDR*)&serverAddress, sizeof(serverAddress));
	listen(serverSocket, 64);

	int addressLength = sizeof(serverAddress);
	while (true) {
		SOCKET clientSocket = socket(AF_INET, SOCK_STREAM, NULL);

		if (clientSocket = accept(serverSocket, (SOCKADDR*)&serverAddress, &addressLength)) {
			Client* client = new Client(clientSocket);
			//cout << "[ ���ο� ����� ���� ]" << endl;
			CreateThread(NULL, NULL, (LPTHREAD_START_ROUTINE)ServerThread, (LPVOID)client, NULL, NULL);
			//connections.push_back(*client);
		}
		Sleep(100);
	}
}