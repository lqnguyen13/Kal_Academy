#include "stdafx.h"
#include <iostream>
#include "Ticket.h"

using namespace std;

//int mTicketNum;
//int mPassengerID;
//int mFlightNum;
//std::string mFlightDate;
//std::string mSeat;


namespace AirlineReservationSystem {

	Ticket::Ticket(int passengerID, int flightNum, const std::string& flightDate) :
		mPassengerID(passengerID), mFlightNum(flightNum), mFlightDate(flightDate)
	{}


	void Ticket::setTicketNum(int ticketNum)
    {
		mTicketNum = ticketNum;
	}

	int Ticket::getTicketNum () const
	{
		return mTicketNum;
	}
	void Ticket::setPassengerID(int passengerID) {
		mPassengerID = passengerID;
	}
	int Ticket::getPassengerID() const {
		return mPassengerID;
	}
	void Ticket::setFlightNum(int flighNum) {
		mFlightNum = flighNum;
	}
	int Ticket::getFlightNum() const {
		return mFlightNum;
	}
	void Ticket::setFlightDate(const std::string& flightDate)
	{
		mFlightDate = flightDate;
	}
	const std::string& Ticket::getFlightDate() const
	{
		return mFlightDate;
	}

	void Ticket::setSeat(const std::string& seat)
	{
		mSeat = seat;
	}
	const std::string& Ticket::getSeat() const
	{
		return mSeat;
	}
	void Ticket::displayTicket()
	{
		cout << "Ticket Information: " << endl;
		cout << "----------------------" << endl;
		cout << "Ticket Number: " << getTicketNum() << endl;
	}

}