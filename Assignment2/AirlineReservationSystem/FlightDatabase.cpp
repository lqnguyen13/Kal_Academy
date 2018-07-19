#include "stdafx.h"
#include <iostream>
#include <stdexcept>
#include "FlightDatabase.h"

using namespace std;

namespace AirlineReservationSystem {
	Flight& FlightDatabase::addFlight(const std::string& depart, const std::string& arrive, const std::string& date,
		const std::string& departTime, const std::string& arriveTime,
		float flightDur, int flightno, int totSeats, int seatsRes)
	{
		Flight theFlight(depart, arrive, date, flightno);
		theFlight.setDepartTime(departTime);
		theFlight.setArriveTime(arriveTime);
		theFlight.setFlightDuration(flightDur);
		theFlight.setTotalSeat(totSeats);
		theFlight.setSeatsReserved(seatsRes);
		mFlights.push_back(theFlight);

		return mFlights[mFlights.size() - 1];
	}

	Flight& FlightDatabase::getFlight(int flightNumber, std::string& flightDate)
	{
		for (auto& flight : mFlights)
		{
			if (flight.getFlightno() == flightNumber && flight.getFlightDate() == flightDate)
			{
				return flight;
			}
		}

		throw logic_error("Flight does not exist.");
	}

	void FlightDatabase::generateFlights()
	{
		addFlight("Seattle", "Los Angeles", "08/12/2018", "08:00", "10:30", 2.5, 1001, 25, 4); // 21 seats available
		addFlight("San Fransisco", "Seattle", "10/01/2018", "13:00", "14:00", 2, 759, 20, 14); // 6 seats available
		addFlight("Las Vegas", "Houston", "11/29/2018", "15:00", "20:00", 3, 93, 18, 6); // 12 seats available
		addFlight("Seattle", "New York", "09/09/2018", "10:00", "17:00", 5, 501, 50, 50); // 0 seats available
		addFlight("Seattle", "New York", "09/16/2018", "10:00", "17:00", 5, 501, 50, 10); // 40 seats available
	}

	void FlightDatabase::displayAllFlights()
	{
		for (auto& flight : mFlights)
		{
			flight.displayFlight();
		}
	}

	void FlightDatabase::displayFlightDetails(Flight flight)
	{
		flight.displayFlight();
	}
}