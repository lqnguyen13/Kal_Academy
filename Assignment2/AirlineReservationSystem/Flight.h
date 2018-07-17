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

	};
}
