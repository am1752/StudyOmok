#pragma once
#include <iostream>
#include <vector>
#include <mutex>
#include "Client.h"

class Client;

class Room {
private:
	int roomID;
	std::vector<Client*> people;
	std::vector<Client*> observer;
	std::string roomName;
	std::vector<std::pair<std::string, std::pair<int, int>>> board;
	std::mutex* mRoom;
public:
	//생성자
	Room(std::string roomName, Client* user);

	//Getter
	int getRoomID();
	int getPeopleNum();
	int getObserverNum();
	int getBoardSize();
	std::vector<Client*> getPeople();
	std::vector<Client*> getObserver();
	std::string getRoomName();
	std::vector <std::pair<std::string, std::pair<int, int>>> getBoard();
	std::mutex* getMutex();

	//Setter
	void setBoard(int x, int y, std::string c);
	void setPeople(std::vector<Client*> people);
	void setMutex(std::mutex* mRoom);

	//함수
	bool joinRoom(Client* user);
	void deleteUser(Client* user);
	void refreshBoard();
};