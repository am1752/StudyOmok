#include <iostream>
#include <vector>
#include <sstream>
#include <mutex>

#include <mysql_connection.h>
#include <cppconn/driver.h>
#include <cppconn/exception.h>
#include <cppconn/prepared_statement.h>

#pragma comment (lib, "ws2_32.lib")
#pragma comment (lib, "mysqlcppconn.lib")
#pragma once

std::vector<std::string> temp;
std::vector<std::string> tokens1;
void getTokens(std::string input, char delimiter) {	
	std::istringstream f(input);
	std::string s;
	temp.clear();
	while (std::getline(f, s, delimiter)) {
		tokens1.push_back(s);
	}
	temp = tokens1;
	tokens1.clear();
	
	//return temp;
}