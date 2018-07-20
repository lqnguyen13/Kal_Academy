#include "stdafx.h"
#include "TicketDatabase.h"

using namespace std;

namespace AirlineReservationSystem {
	Ticket& TicketDatabase::addTicket(const int selectedFlightNum, const std::string& selectedFlightDate, const int passengerId)
	{
		Ticket theTicket(passengerId, selectedFlightNum, selectedFlightDate);
		theTicket.setTicketNum(mNextTicketNumber++);
		mTickets.push_back(theTicket);

		return mTickets[mTickets.size() - 1];
	}

	Ticket& TicketDatabase::getTicketByTicketNum(int ticketNumber)
	{
		for (auto& ticket : mTickets)
		{
			if (ticket.getTicketNum() == ticketNumber)
			{
				return ticket;
			}
		}

		throw logic_error("Ticket does not exist.");
	}

	Ticket& TicketDatabase::getTicketByPassengerNum(int passengerID)
	{
		for (auto& ticket : mTickets)
		{
			if (ticket.getPassengerID() == passengerID)
			{
				return ticket;
			}
		}

		throw logic_error("Ticket does not exist.");
	}

	void TicketDatabase::display(Ticket ticket) const
	{
		ticket.displayTicket();
	}

}