#include "Client.h"
#include <mutex>

//생성자
Client::Client(SOCKET clientSocket) {
	this->roomID = -1;
	this->clientSocket = clientSocket;
	this->location = "Lobby";
	this->ready = false;
}

//Getter
std::string Client::getClientID() {
	return clientID;
}
bool Client::getPlayer() {
	return player;
}
int Client::getRoomID() {
	return roomID;
}
SOCKET Client::getClientSocket() {
	return clientSocket;
}
std::string Client::getLocation() {
	return location;
}
bool Client::getReady() {
	return this->ready;
}
Room* Client::getMyRoom() {
	return this->myRoom;
}
std::mutex* Client::getMutex() {
	return this->mClient;
}
//Setter
bool Client::setClientID(std::string client, std::vector<Client*> connections) {
	mClient->lock();
	for (int i = 0; i < connections.size(); i++) {
		if (client == connections[i]->getClientID()) {
			mClient->unlock();
			return false;
		}
	}
	mClient->unlock();
	this->clientID = client;
	return true;
}
void Client::setPlayer(bool player) {
	this->player = player;
}
void Client::setRoomID(int roomID) {
	this->roomID = roomID;
}
void Client::setLocation(std::string location) {
	this->location = location;
}
void Client::setReady(bool ready) {
	this->ready = ready;
}
void Client::setMyRoom(Room* room) {
	this->myRoom = room;
}
void Client::setMutex(std::mutex* mClient) {
	this->mClient = mClient;
}
//함수