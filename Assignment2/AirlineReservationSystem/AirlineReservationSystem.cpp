// AirlineReservationSystem.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <algorithm> // for use of std::transform method for converting strings to all lowercase
#include "FlightDatabase.h" 
#include "PassengerDatabase.h"
#include "TicketDatabase.h"

using namespace std;
using namespace AirlineReservationSystem;

int displayMainMenu();

Flight& getAFlight(FlightDatabase& flightDB);

void doDisplayOneFlight(FlightDatabase& flightDB);

void makeReservation(FlightDatabase& flightDB, PassengerDatabase& passengerDB, TicketDatabase& ticketDB);

Passenger& addAPassenger(PassengerDatabase& passengerDB);

void searchForTicket(TicketDatabase& ticketDB, PassengerDatabase& passengerDB, FlightDatabase& flightDB);

void fightAvailableSeats(FlightDatabase& db, int flightNumber, string flightDate);


int main()
{
	FlightDatabase flightDB;
	PassengerDatabase passengerDB;
	TicketDatabase ticketDB;

	flightDB.generateFlights();

	while (true) {
		int selection = displayMainMenu();
		switch (selection) {
		case 0:
			return 0;
		case 1:
			cout << "FLIGHT SCHEDULE: " << endl;
			flightDB.displayAllFlights();
			break;
		case 2:
			doDisplayOneFlight(flightDB);
			break;
		case 3:
			makeReservation(flightDB, passengerDB, ticketDB);
			break;
		case 4:
			cout << "All passengers: " << endl;
			passengerDB.displayAll();
			break;
		case 5:
			searchForTicket(ticketDB, passengerDB, flightDB);
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

Flight& getAFlight(FlightDatabase& flightDB)
{
	int flightNumber;
	string flightDate;

	cout << "   Flight number? ";
	cin >> flightNumber;
	cout << "   Flight Date (MM/DD/YYYY)? ";
	cin >> flightDate;

	Flight& thisFlight = flightDB.getFlight(flightNumber, flightDate);
	// add error handling if flight does not exist (getFlight method already throws logic_error)

	return thisFlight;
}

void doDisplayOneFlight(FlightDatabase& flightDB)
{

	Flight& thisFlight = getAFlight(flightDB);
	// add error handling if flight does not exist (getFlight method already throws logic_error)

	flightDB.displayFlightDetails(thisFlight);
}

Passenger& addAPassenger(PassengerDatabase& passengerDB)
{
	string fName;
	string lName;
	string pPhoneNumber;
	string pEmail;

	cout << "   First Name? ";
	cin >> fName;
	cout << "   Last Name? ";
	cin >> lName;
	cout << " Phone Number? ";
	cin >> pPhoneNumber;
	cout << " Email? ";
	cin >> pEmail;

	Passenger& newPassenger = passengerDB.addPassenger(fName, lName, pPhoneNumber, pEmail);
	return newPassenger;
}

// TODO add tests for results 
void makeReservation(FlightDatabase& flightDB, PassengerDatabase& passengerDB, TicketDatabase& ticketDB)
{

	int flightNumber;
	string flightDate;

	/*cout << "Which flight do you want? " << endl;
	cin >> flightNumber;
	cout << "Flight date? " << endl;
	cin >> flightDate;*/
	// TODO error handling if flight does not exist -- getFlight method in FlightDatabase already thorws a logic error - need to add handling
	Flight& thisFlight = getAFlight(flightDB);

	flightNumber = thisFlight.getFlightno();
	flightDate = thisFlight.getFlightDate();
	try
	{
		//Flight& fli = flightDB.getFlight(flightNumber, flightDate);
		if (thisFlight.getFlightno() == flightNumber and thisFlight.getFlightDate() == flightDate)
		{
			thisFlight.displayFlight();
			thisFlight.getFlightno();
			thisFlight.getFlightDate();
		}		
	}
	catch (const logic_error& exception)
	{
		cerr << "Flight does not exist: " << exception.what() << endl;
	}
	
	// TODO error handling if flight does not have available seats
	fightAvailableSeats(flightDB, flightNumber, flightDate);

	// TODO if flight exists and has seats available, display # of seats available and confirm that user wants to make a reservation, then ask for passenger info

	cout << "Who is the passenger? " << endl;
	Passenger& thePassenger = addAPassenger(passengerDB);

	// TODO check if passenger already exists because we don't want to have duplicate passenger entries (maybe do this in addAPassenger method?)

	Ticket& theTicket = ticketDB.addTicket(thisFlight.getFlightno(), thisFlight.getFlightDate(), thePassenger.getPassengerID());
	int numSeatsRes = thisFlight.getSeatsReserved();
	thisFlight.setSeatsReserved(numSeatsRes++);

	cout << "You have successfully reserved a seat on flight number " << thisFlight.getFlightno() << " on "
		<< thisFlight.getFlightDate() << ". Your ticket number is " << theTicket.getTicketNum() << "." << endl;

	//thePassenger.display();
	//theTicket.display();
	//thisFlight.displayFlight();
}

void searchForTicket(TicketDatabase& ticketDB, PassengerDatabase& passengerDB, FlightDatabase& flightDB)
{
	int ticketNum;
	string searchTicketNum;
	string fName;
	string lName;
	Ticket selectedTicket;
	Passenger selectedPassenger;
	Flight selectedFlight;
	int pid;
	int flightNum;
	string flightDate;

	cout << "You can search by ticket number or passenger first and last name." << endl;
	cout << "Search by ticket number? (Y/N) ";
	cin >> searchTicketNum;

	transform(searchTicketNum.begin(), searchTicketNum.end(), searchTicketNum.begin(), tolower);

	try
	{
		if (searchTicketNum == "y")
		{
			cout << "   Enter ticket number: ";
			cin >> ticketNum;

			// get ticket object, then get passenger by passenger ID in ticket object
			selectedTicket = ticketDB.getTicketByTicketNum(ticketNum);
			pid = selectedTicket.getPassengerID();
			selectedPassenger = passengerDB.getPassenger(pid);

		}
		else if (searchTicketNum == "n")
		{
			cout << "   Enter passenger first name: ";
			cin >> fName;
			cout << "   Enter passenger last name: ";
			cin >> lName;

			transform(fName.begin(), fName.end(), fName.begin(), tolower);
			transform(lName.begin(), lName.end(), lName.begin(), tolower);

			// get passenger object, then get ticket by passenger ID in passenger object
			selectedPassenger = passengerDB.getPassenger(fName, lName);
			pid = selectedPassenger.getPassengerID();
			selectedTicket = ticketDB.getTicketByPassengerNum(pid);
		}
	}
	catch (const std::logic_error& exception)
	{
		cout << "Unable to display ticket: " << exception.what() << endl;
		return;
	}

	try 
	{
		// get flight data
		flightNum = selectedTicket.getFlightNum();
		flightDate = selectedTicket.getFlightDate();
		selectedFlight = flightDB.getFlight(flightNum, flightDate);
		// display ticket, passenger, and flight data
		selectedTicket.displayTicket();
		selectedPassenger.display();
		selectedFlight.displayFlight();
	}
	catch (const std::logic_error& exception)
	{
		cout << "Unable to display ticket: " << exception.what() << endl;
	}
}

void fightAvailableSeats(FlightDatabase& db, int flightNumber, string flightDate) {

	int noSeats;
	cout << "Enter number of seats? ";
	cin >> noSeats;
	try
	{
		Flight& flight = db.getFlight(flightNumber, flightDate);
		if (flight.getAvailableSeats() >= noSeats and flight.getAvailableSeats() > 0)
		{
			int rem = flight.getTotalSeat() - noSeats;
			cout << "You have " << noSeats << " reserved seat(s)." << endl;
			cout << "Seats remaining " << rem << endl;
		}
		else {
			cout << noSeats << " unavailable seat(s)." << endl;
		}
	}
	catch (const logic_error& exception)
	{
		cerr << "No seats available: " << exception.what() << endl;
	}
}

