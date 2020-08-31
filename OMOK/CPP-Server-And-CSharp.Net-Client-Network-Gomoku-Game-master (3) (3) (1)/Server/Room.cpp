#include "Room.h"
#include <mutex>

int roomNum = 0;

//생성자
Room::Room(std::string roomName, Client* user) {
	people.push_back(user);
	this->roomName = roomName;
	this->roomID = roomNum++;
}

//Getter
int Room::getRoomID() {
	return roomID;
}
std::string Room::getRoomName() {
	return roomName;
}
int Room::getPeopleNum() {
	return people.size();
}
int Room::getObserverNum() {
	return observer.size();
}
std::vector<Client*> Room::getPeople() {
	return people;
}
std::vector<Client*> Room::getObserver() {
	return observer;
}
int Room::getBoardSize() {
	return board.size();
}
std::vector <std::pair<std::string, std::pair<int, int>>> Room::getBoard() {
	return board;
}

std::mutex* Room::getMutex() {
	return this->mRoom;
}
//Setter
void Room::setPeople(std::vector<Client*> people) {
	this->people = people;
}
void Room::setBoard(int x, int y, std::string c) {
	board.push_back(std::make_pair(c, std::make_pair(x, y)));
}
void Room::setMutex(std::mutex* mRoom) {
	this->mRoom = mRoom;
}
//함수
bool Room::joinRoom(Client* user) {
	if (people.size() < 2) {
		people.push_back(user);
		user->setPlayer(true);
		return true;
	}
	else {
		observer.push_back(user);
		user->setPlayer(false);
		return true;
	}
	return false;
}
void Room::deleteUser(Client* user) {
	if (user->getPlayer() == true) {
		for (int i = 0; i < people.size(); i++) {
			if (people[i]->getClientID() == user->getClientID()) {
				people.erase(people.begin() + i);
				break;
			}
		}
	}
	else {
		for (int i = 0; i < observer.size(); i++) {
			if (observer[i]->getClientID() == user->getClientID()) {
				observer.erase(observer.begin() + i);
				break;
			}
		}
	}
}

void Room::refreshBoard() {
	board.clear();
}