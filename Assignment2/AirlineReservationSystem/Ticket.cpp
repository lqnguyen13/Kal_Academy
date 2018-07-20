#include "stdafx.h"
#include "Ticket.h"

using namespace std;

//int mTicketNum;
//int mPassengerID;
//int mFlightNum;
//std::string mFlightDate;
//std::string mSeat;


namespace AirlineReservationSystem {

	Ticket::Ticket(int& ticketNum, int& passengerID, int& flightNum, const std::string& flightDate, const std::string& seat) :
		mTicketNum(ticketNum), mPassengerID(passengerID), mFlightNum(flightNum), mFlightDate(flightDate), mSeat(seat)
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

	void Ticket::setSeat(const std::string& seat)
	{
		mSeat = seat;
	}
	const std::string& Ticket::getSeat() const
	{
		return mSeat;
	}

}