#pragma once
#include <vector>
#include "Flight.h"

namespace AirlineReservationSystem {
	class FlightDatabase
	{
	private:
		std::vector<Flight> mFlights;

	public:
		Flight& addFlight(const std::string& depart, const std::string& arrive,
			const std::string& departTime, const std::string& arriveTime, const std::string& date,
			float flightDur, int flightno, int totSeats, int seatsRes);
		Flight& getFlight(int flightNumber, std::string& flightDate);
		
		void generateFlights();
		void displayAllFlights(); // use this to display flight schedule
		void displayFlightDetails(Flight flight); // use this to display one flight
	};
}
