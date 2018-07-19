#include "stdafx.h"
#include "Flight.h"
#include <iostream>


using namespace std;

namespace AirlineReservationSystem
{
	Flight::Flight(const string& depart, const string& arrive, const int flightno)
		: mDepartLoc(depart), mArriveLoc(arrive), mFlightNum(flightno) {}



	void Flight::setDepartLoc(const string& depart) {
		mDepartLoc = depart;
	}

	const string& Flight::getDepartLoc() const {
		return mDepartLoc;
	}

	void Flight::setArriveLoc(const string& arrive) {
		mArriveLoc = arrive;
	}

	const string& Flight::getArriveLoc() const {
		return mArriveLoc;
	}

	void Flight::setDepartTime(const std::string & departtime)
	{
		mDepartTime = departtime;
	}

	const std::string & Flight::getDepartTime() const
	{
		return mDepartTime;
	}

	void Flight::setArriveTime(const std::string & arrivetime)
	{
		mArriveTime = arrivetime;
	}

	const std::string & Flight::getArriveTime() const
	{
		return mArriveTime;
	}

	void Flight::setFlightDuration(float duration)
	{
		mFlightDur = duration;
	}

	float Flight::getFlightDuration() const
	{
		return mFlightDur;
	}

	void Flight::setTotalSeat(int noofseat)
	{
		mTotalSeats = noofseat;
	}

	int Flight::getTotalSeat() const
	{
		return mTotalSeats;
	}


	bool Flight::isReserved() const
	{
		return mReserved;
	}

	void Flight::setFlightno(int flightno) {
		mFlightNum = flightno;
	}

	int Flight::getFlightno() const {
		return mFlightNum;
	}


		void Flight::displayFlight() const {
			cout << "FlightNo: " << getFlightno() << endl;
			cout << "Arrival Loc:  " << getArriveLoc() << endl;
			cout << "Depart Loc: " << getDepartLoc() << endl;
			cout << "-------------------------" << endl;
		}




	}


