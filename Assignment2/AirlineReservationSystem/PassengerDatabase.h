#pragma once
#include <vector>
#include "Passenger.h"

namespace AirlineReservationSystem {
	const int kFirstPassengerNumber = 1000;

	class PassengerDatabase
	{
	private:
		std::vector<Passenger> mPassengers;
		int mNextPassengerNumber = kFirstPassengerNumber;

	public:
		// TODO: WRITE SETTERS/GETTERS AND METHODS IN HEADER FILE

	};
}
