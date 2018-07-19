#pragma once
#include <string>

namespace AirlineReservationSystem {
	class Flight
	{
	private:
		int mFlightNum;
		std::string mFlightDate;
		std::string mDepartLoc;
		std::string mArriveLoc;
		std::string mDepartTime;
		std::string mArriveTime;
		float mFlightDur;
		int mTotalSeats;
		int mSeatsReserved;
		bool mReserved = false;

	public:
		// TODO: WRITE SETTERS/GETTERS AND METHODS IN HEADER FILE

		Flight() = default;
		Flight(const std::string& depart,
			const std::string& arrive,
			int flightno);

	

		//Getters and setters


		bool isReserved() const;

		void setFlightno(int flightno);
		void displayFlight() const;
		int getFlightno() const;

		void setDepartLoc(const std::string& depart);
		const std::string& getDepartLoc() const;

		void setArriveLoc(const std::string& arrive);
		const std::string& getArriveLoc() const;

		void setDepartTime(const std::string& departtime);
		const std::string& getDepartTime() const;

		void setArriveTime(const std::string& arrivetime);
		const std::string& getArriveTime() const;

		void setFlightDuration(float duration);
		float getFlightDuration() const;

		void setTotalSeat(int noofseat);
		int getTotalSeat() const;
	};
}
