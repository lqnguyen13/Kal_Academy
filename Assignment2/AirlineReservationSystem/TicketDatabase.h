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
		Ticket& addTicket(const int selectedFlightNum, const std::string& selectedFlightDate, const std::string& passFirstName, const std::string& passLastName); // Creates a ticket and adds to mTickets vector
		Ticket& getTicket(int ticketNumber);
		Ticket& getTicket(const std::string& passFirstName, const std::string& passLastName);
		
		void displayTicket() const; //Not sure if we need this
	};
}