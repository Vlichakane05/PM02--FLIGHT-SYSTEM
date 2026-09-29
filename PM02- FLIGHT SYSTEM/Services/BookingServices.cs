using PM02__FLIGHT_SYSTEM.Models;

namespace PM02__FLIGHT_SYSTEM.Services
{
    public class BookingService
    {
        private readonly DatabaseService databaseService;

        public BookingService(DatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        // =========================================
        // GET ALL FLIGHTS
        // =========================================

        public List<Flight> GetFlights()
        {
            return databaseService.GetFlights();
        }

        // =========================================
        // GET FLIGHT BY ID
        // =========================================

        public Flight? GetFlightByID(int flightID)
        {
            List<Flight> flights =
                databaseService.GetFlights();

            return flights.FirstOrDefault(
                f => f.FlightID == flightID);
        }

        // =========================================
        // GET AIRPORT
        // =========================================

        public Airport? GetAirportByID(int airportID)
        {
            return databaseService.GetAirportByID(
                airportID);
        }

        // =========================================
        // GET PASSENGER
        // =========================================

        public Passenger? GetPassengerByID(
            int passengerID)
        {
            return databaseService.GetPassengerByID(
                passengerID);
        }

        // =========================================
        // ADD PASSENGER
        // =========================================

        public Passenger AddPassenger(
            string firstName,
            string lastName,
            string email,
            string phone,
            string passportNumber)
        {
            return databaseService.AddPassenger(
                firstName,
                lastName,
                email,
                phone,
                passportNumber);
        }

        // =========================================
        // CHECK SEAT
        // =========================================

        public bool IsSeatAvailable(
            int flightID,
            string seatNumber)
        {
            return databaseService.IsSeatAvailable(
                flightID,
                seatNumber);
        }

        // =========================================
        // CREATE PAYMENT
        // =========================================

        public int CreatePayment(
            decimal amount,
            string paymentMethod)
        {
            return databaseService.CreatePayment(
                amount,
                paymentMethod);
        }

        // =========================================
        // CREATE BOOKING
        // =========================================

        public int CreateBooking(
            int passengerID,
            int flightID,
            int paymentID)
        {
            return databaseService.CreateBooking(
                passengerID,
                flightID,
                paymentID);
        }

        // =========================================
        // CREATE TICKET
        // =========================================

        public string CreateTicket(
            int bookingID,
            string seatNumber)
        {
            return databaseService.CreateTicket(
                bookingID,
                seatNumber);
        }

        // =========================================
        // GET BOOKING
        // =========================================

        public Booking? GetBookingByID(
            int bookingID)
        {
            return databaseService.GetBookingByID(
                bookingID);
        }

        // =========================================
        // CANCEL BOOKING
        // =========================================

        public bool CancelBooking(int bookingID)
        {
            return databaseService.CancelBooking(
                bookingID);
        }
    }
}