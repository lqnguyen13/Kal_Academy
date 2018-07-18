#include "stdafx.h"
#include <iostream>
#include "Passenger.h"

using namespace std;

namespace AirlineReservationSystem {
	Passenger::Passenger(const string& firstName, const string& lastName)
		: mFirstName(firstName), mLastName(lastName) {}

	Passenger::Passenger(const string& firstName, const string& lastName,
		const string& phoneNumber, const string& email) : mFirstName(firstName),
		mLastName(lastName), mPhoneNumber(phoneNumber), mEmail(email) {}

	void Passenger::setFirstName(const string& firstName) {
		mFirstName = firstName;
	}

	const string& Passenger::getFirstName() const {
		return mFirstName;
	}

	void Passenger::setLastName(const string& lastName) {
		mLastName = lastName;
	}

	const string& Passenger::getLastName() const {
		return mLastName;
	}

	void Passenger::setPhoneNumber(const string& phoneNumber) {
		mPhoneNumber = phoneNumber;
	}

	const string& Passenger::getPhoneNumber() const {
		return mPhoneNumber;
	}

	void Passenger::setEmail(const string& email) {
		mEmail = email;
	}

	const string& Passenger::getEmail() const {
		return mEmail;
	}

	void Passenger::setPassengerID(int passengerID) {
		mPassengerID = passengerID;
	}

	int Passenger::getPassengerID() const {
		return mPassengerID;
	}

	void Passenger::addPassenger() {
		mAdded = true;
	}

	void Passenger::removePassenger() {
		mAdded = false;
	}

	bool Passenger::isAdded() const {
		return mAdded;
	}

	void Passenger::display() const {
		cout << "Passenger: " << getFirstName() << ", " << getFirstName() << endl;
		cout << "Passenger ID: " << getPassengerID() << endl;
		cout << "Passenger Email: " << getEmail() << endl;
		cout << "Passenger Phone Number: " << getPhoneNumber() << endl;
		cout << "-------------------------" << endl;
	}
}