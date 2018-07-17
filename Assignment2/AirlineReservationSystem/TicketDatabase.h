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
		// TODO: WRITE SETTERS/GETTERS AND METHODS IN HEADER FILE

	};
}