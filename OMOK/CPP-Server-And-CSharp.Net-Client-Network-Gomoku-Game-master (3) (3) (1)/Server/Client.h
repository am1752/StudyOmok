#pragma once
#include <iostream>
#include <Windows.h>
#include <mutex>
#include "Room.h"

class Room;

class Client {
private:
	SOCKET clientSocket;
	std::string clientID;
	std::string location;
	int roomID;
	bool ready;
	bool player;
	Room* myRoom;
	std::mutex* mClient;
public:
	//생성자
	Client(SOCKET clientSocket);

	//Getter
	SOCKET getClientSocket();
	std::string getLocation();
	
	int getRoomID();
	bool getPlayer();
	bool getReady();
	std::string getClientID();
	Room* getMyRoom();
	std::mutex* getMutex();
	//Setter
	void setRoomID(int roomID);
	void setPlayer(bool player);
	bool setClientID(std::string client, std::vector<Client*> connections);
	void setLocation(std::string location);
	void setReady(bool ready);
	void setMyRoom(Room* room);
	void setMutex(std::mutex* mClient);
	//함수
};