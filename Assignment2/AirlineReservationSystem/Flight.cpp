#include "stdafx.h"
#include "Flight.h"
#include <iostream>


using namespace std;

namespace AirlineReservationSystem
{
	Flight::Flight(const string& depart, const string& arrive, const string& date, const int flightno)
		: mDepartLoc(depart), mArriveLoc(arrive), mFlightDate(date), mFlightNum(flightno) {}



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

	void Flight::setDepartTime(const std::string& departtime)
	{
		mDepartTime = departtime;
	}

	const std::string& Flight::getDepartTime() const
	{
		return mDepartTime;
	}

	void Flight::setArriveTime(const std::string& arrivetime)
	{
		mArriveTime = arrivetime;
	}

	const std::string& Flight::getArriveTime() const
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

	void Flight::setFlightDate(int flightDate)
	{
		mFlightDate = flightDate;
	}
	const std::string& Flight::getFlightDate() const
	{
		return mFlightDate;
	}
	void Flight::setSeatsReserved(int resSeats)
	{
		mSeatsReserved = resSeats;
	}
	int Flight::getSeatsReserved()
	{
		return mSeatsReserved;
	}
	int Flight::getAvailableSeats()
	{
		return getTotalSeat() - getSeatsReserved();
	}

	void Flight::displayFlight() {
		cout << "FlightNo: " << getFlightno() << endl;
		cout << "Flight Date: " << getFlightDate() << endl;
		cout << "Depart Loc: " << getDepartLoc() << endl;
		cout << "Depart Time: " << getDepartTime() << endl;
		cout << "Arrival Loc:  " << getArriveLoc() << endl;
		cout << "Arrival Time: " << getArriveTime() << endl;
		cout << "Flight Duration (hrs): " << getFlightDuration() << endl;
		cout << "Total Seats Available: " << getAvailableSeats() << endl;
		cout << "-------------------------" << endl;
	}




}


