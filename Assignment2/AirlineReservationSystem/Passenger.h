#pragma once
#include <string>

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

	};
}
