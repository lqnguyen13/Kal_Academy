#pragma once
#include <vector>
#include "Flight.h"

namespace AirlineReservationSystem {
	class FlightDatabase
	{
	private:
		std::vector<Flight> mFlights;

	public:
		Flight& getFlight(int flightNumber, std::string& flightDate);
		
		void displayAllFlights() const;
	};
}
