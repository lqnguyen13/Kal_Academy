#pragma once
#include <string>
#include <vector>
#include "Ticket.h"

namespace AirlineReservationSystem {
	class Passenger
	{
	private:
		int mPassengerID = -1;
		std::string mFirstName;
		std::string mLastName;
		std::string mPhoneNumber;
		std::string mEmail;
		bool mAdded = false;

	public:
		// TODO: WRITE SETTERS/GETTERS AND METHODS IN HEADER FILE
		Passenger() = default;
		Passenger(const std::string& firstName, const std::string& lastName);
		Passenger(const std::string& passFirstName, const std::string& passLastName, const std::string& passPhoneNum, const std::string& passEmail);

		void addPassenger();
		void removePassenger();
		void display() const;

		void setFirstName(const std::string& firstName);
		const std::string& getFirstName() const;

		void setLastName(const std::string& lastName);
		const std::string& getLastName() const;

		void setPhoneNumber(const std::string& phoneNumber);
		const std::string& getPhoneNumber() const;

		void setEmail(const std::string& email);
		const std::string& getEmail() const;

		void setPassengerID(int passengerID);
		int getPassengerID() const;

		bool isAdded() const;
	};
}