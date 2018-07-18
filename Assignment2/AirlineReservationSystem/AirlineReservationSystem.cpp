// AirlineReservationSystem.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include "FlightDatabase.h" 
#include "PassengerDatabase.h"
#include "TicketDatabase.h"

using namespace std;
using namespace AirlineReservationSystem;

int displayMainMenu();

Flight getAFlight(FlightDatabase& flightDB);

void doDisplayOneFlight(FlightDatabase& flightDB);

void makeReservation(FlightDatabase& flightDB, PassengerDatabase& passengerDB, TicketDatabase& ticketDB);

Passenger addAPassenger(PassengerDatabase& passengerDB);


int main()
{
	FlightDatabase flightDB;
	PassengerDatabase passengerDB;
	TicketDatabase ticketDB;

	while (true) {
		int selection = displayMainMenu();
		switch (selection) {
		case 0:
			return 0;
		case 1:
	//		flightDB.displayAllFlights();
			cout << "This will display all of the flights" << endl;
			break;
		case 2:
			doDisplayOneFlight(flightDB);
			break;
		case 3:
			makeReservation(flightDB, passengerDB, ticketDB);
			break;
		case 4:
	//		passengerDB.displayAll();
			cout << "Display all passengers." << endl;
			break;
		case 5:
//			ticketDB.display();
			break;
		default:
			cerr << "Unknown command." << endl;
			break;
		}
	}

    return 0;
}

int displayMainMenu()
{
	int selection;

	cout << endl;
	cout << "Airline Reservation System" << endl;
	cout << "--------------------------" << endl;
	cout << "1) Display Flight Schedule" << endl;
	cout << "2) Display a Specific Flight" << endl;
	cout << "3) Make a Flight Reservation" << endl;
	cout << "4) Display All Passengers" << endl;
	cout << "5) Display Ticket Information" << endl;
	cout << "0) Quit" << endl;
	cout << endl;
	cout << "---> ";

	cin >> selection;

	return selection;
}

Flight getAFlight(FlightDatabase& flightDB)
{
	int flightNumber;
	string flightDate;

	cout << "   Flight number? ";
	cin >> flightNumber;
	cout << "   Flight Date? ";
	cin >> flightDate;

	Flight thisFlight;

// thisFlight = flightDB.getFlight(flightNumber, flightDate);

	return thisFlight;
}

void doDisplayOneFlight(FlightDatabase& flightDB)
{

	Flight thisFlight = getAFlight(flightDB);
//	flightDB.displayFlightDetails(thisFlight);
}

Passenger addAPassenger(PassengerDatabase& passengerDB)
{
	string fName;
	string lName;

	cout << "   First Name? ";
	cin >> fName;
	cout << "   Last Name? ";
	cin >> lName;

	Passenger newPassenger;
	// newPassenger = passengerDB.addPassenger(fName, lName);
	return newPassenger;
}

// TODO add tests for results 
void makeReservation(FlightDatabase& flightDB, PassengerDatabase& passengerDB, TicketDatabase& ticketDB)
{
	cout << "Which flight do you want? ";
	Flight thisFlight = getAFlight(flightDB);

	cout << "Who is the passenger? ";
	Passenger thePassenger = addAPassenger(passengerDB);

//	Ticket theTicket = ticketDB.addTicket(thisFlight.getFlightno(), thisFlight.getFlightDate(), thePassenger.getFirstName(), thePassenger.getLastName());

	//thePassenger.display();
	//theTicket.display();
	//thisFlight.displayFlight();
}

