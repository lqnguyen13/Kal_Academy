#pragma once
#include <vector>
#include "Ticket.h"

namespace AirlineReservationSystem {
	const int kFirstTicketNumber = 1000;

	class TicketDatabase
	{
	private:
		std::vector<Ticket> mTickets;
		int mNextTicketNumber = kFirstTicketNumber;

	public:
		Ticket& addTicket(const int selectedFlightNum, const std::string& selectedFlightDate, const int passengerId); // Creates a ticket and adds to mTickets vector
		Ticket& getTicketByTicketNum(int ticketNumber);
		Ticket& getTicketByPassengerNum(int passengerId);
		
		void display(Ticket ticket) const;
	};
}