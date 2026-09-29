using PM02__FLIGHT_SYSTEM.Models;
using PM02__FLIGHT_SYSTEM.Services;

namespace PM02__FLIGHT_SYSTEM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DatabaseService databaseService =
                new DatabaseService();

            BookingService bookingService =
                new BookingService(databaseService);

            bool running = true;

            while (running)
            {
                Console.Clear();

                Console.WriteLine("=================================");
                Console.WriteLine("       FLIGHT BOOKING SYSTEM     ");
                Console.WriteLine("=================================");
                Console.WriteLine();
                Console.WriteLine("1. View Flights");
                Console.WriteLine("2. Search Flights");
                Console.WriteLine("3. Make Booking");
                Console.WriteLine("4. View Booking");
                Console.WriteLine("5. Cancel Booking");
                Console.WriteLine("6. Exit");
                Console.WriteLine();

                Console.Write("Select an option: ");
                string choice =
                    Console.ReadLine() ?? "";

                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ViewFlights(bookingService);
                        break;

                    case "2":
                        SearchFlights(bookingService);
                        break;

                    case "3":
                        MakeBooking(bookingService);
                        break;

                    case "4":
                        ViewBooking(bookingService);
                        break;

                    case "5":
                        CancelBooking(bookingService);
                        break;

                    case "6":
                        running = false;
                        Console.WriteLine(
                            "Goodbye!");
                        break;

                    default:
                        Console.WriteLine(
                            "Invalid option.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine();
                    Console.WriteLine(
                        "Press Enter to return to the menu...");
                    Console.ReadLine();
                }
            }
        }

        // =========================================
        // VIEW FLIGHTS
        // =========================================

        static void ViewFlights(
            BookingService bookingService)
        {
            Console.WriteLine("AVAILABLE FLIGHTS");
            Console.WriteLine("=================");

            List<Flight> flights =
                bookingService.GetFlights();

            if (flights.Count == 0)
            {
                Console.WriteLine(
                    "No flights available.");

                return;
            }

            foreach (Flight flight in flights)
            {
                Airport? departure =
                    bookingService.GetAirportByID(
                        flight.DepartureAirportID);

                Airport? arrival =
                    bookingService.GetAirportByID(
                        flight.ArrivalAirportID);

                string departureCode =
                    departure?.AirportCode ?? "N/A";

                string arrivalCode =
                    arrival?.AirportCode ?? "N/A";

                Console.WriteLine();
                Console.WriteLine(
                    $"Flight ID: {flight.FlightID}");

                Console.WriteLine(
                    $"Route: {departureCode} -> {arrivalCode}");

                Console.WriteLine(
                    $"Departure: {flight.DepartureDateTime}");

                Console.WriteLine(
                    $"Arrival: {flight.ArrivalDateTime}");

                Console.WriteLine(
                    $"Price: R{flight.Price:F2}");

                Console.WriteLine(
                    $"Status: {flight.Status}");

                Console.WriteLine("-----------------------------");
            }
        }

        // =========================================
        // SEARCH FLIGHTS
        // =========================================

        static void SearchFlights(
            BookingService bookingService)
        {
            Console.WriteLine("SEARCH FLIGHTS");
            Console.WriteLine("==============");

            Console.Write(
                "Departure airport code " +
                "(e.g. JNB), or Enter for any: ");

            string departureAirport =
                Console.ReadLine()?
                .Trim()
                .ToUpper() ?? "";

            Console.Write(
                "Arrival airport code " +
                "(e.g. CPT), or Enter for any: ");

            string arrivalAirport =
                Console.ReadLine()?
                .Trim()
                .ToUpper() ?? "";

            List<Flight> flights =
                bookingService.GetFlights();

            List<Flight> matchingFlights =
                new List<Flight>();

            foreach (Flight flight in flights)
            {
                Airport? departure =
                    bookingService.GetAirportByID(
                        flight.DepartureAirportID);

                Airport? arrival =
                    bookingService.GetAirportByID(
                        flight.ArrivalAirportID);

                bool departureMatches =
                    string.IsNullOrEmpty(
                        departureAirport) ||
                    (departure != null &&
                     departure.AirportCode.ToUpper() ==
                     departureAirport);

                bool arrivalMatches =
                    string.IsNullOrEmpty(
                        arrivalAirport) ||
                    (arrival != null &&
                     arrival.AirportCode.ToUpper() ==
                     arrivalAirport);

                if (departureMatches &&
                    arrivalMatches)
                {
                    matchingFlights.Add(flight);
                }
            }

            Console.WriteLine();

            if (matchingFlights.Count == 0)
            {
                Console.WriteLine(
                    "No flights found.");

                return;
            }

            Console.WriteLine("SEARCH RESULTS");
            Console.WriteLine("==============");

            foreach (Flight flight in matchingFlights)
            {
                Airport? departure =
                    bookingService.GetAirportByID(
                        flight.DepartureAirportID);

                Airport? arrival =
                    bookingService.GetAirportByID(
                        flight.ArrivalAirportID);

                Console.WriteLine(
                    $"Flight {flight.FlightID}: " +
                    $"{departure?.AirportCode ?? "N/A"} -> " +
                    $"{arrival?.AirportCode ?? "N/A"} | " +
                    $"{flight.DepartureDateTime} | " +
                    $"R{flight.Price:F2}");
            }
        }

        // =========================================
        // MAKE BOOKING
        // =========================================

        static void MakeBooking(
            BookingService bookingService)
        {
            Console.WriteLine("MAKE BOOKING");
            Console.WriteLine("============");

            List<Flight> flights =
                bookingService.GetFlights();

            if (flights.Count == 0)
            {
                Console.WriteLine(
                    "No flights available.");

                return;
            }

            Console.WriteLine();
            Console.WriteLine("AVAILABLE FLIGHTS");

            foreach (Flight flight in flights)
            {
                Airport? departure =
                    bookingService.GetAirportByID(
                        flight.DepartureAirportID);

                Airport? arrival =
                    bookingService.GetAirportByID(
                        flight.ArrivalAirportID);

                Console.WriteLine(
                    $"{flight.FlightID}. " +
                    $"{departure?.AirportCode ?? "N/A"} -> " +
                    $"{arrival?.AirportCode ?? "N/A"} | " +
                    $"R{flight.Price:F2}");
            }

            Console.WriteLine();

            Console.Write(
                "Enter Flight ID: ");

            if (!int.TryParse(
                Console.ReadLine(),
                out int flightID))
            {
                Console.WriteLine(
                    "Invalid Flight ID.");

                return;
            }

            Flight? selectedFlight =
                bookingService.GetFlightByID(
                    flightID);

            if (selectedFlight == null)
            {
                Console.WriteLine(
                    "Flight not found.");

                return;
            }

            Console.WriteLine();
            Console.WriteLine("PASSENGER INFORMATION");

            Console.Write("First Name: ");
            string firstName =
                Console.ReadLine() ?? "";

            Console.Write("Last Name: ");
            string lastName =
                Console.ReadLine() ?? "";

            Console.Write("Email: ");
            string email =
                Console.ReadLine() ?? "";

            Console.Write("Phone: ");
            string phone =
                Console.ReadLine() ?? "";

            Console.Write("Passport Number: ");
            string passportNumber =
                Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(passportNumber))
            {
                Console.WriteLine();
                Console.WriteLine(
                    "All passenger fields are required.");

                return;
            }

            Passenger passenger =
                bookingService.AddPassenger(
                    firstName,
                    lastName,
                    email,
                    phone,
                    passportNumber);

            Console.WriteLine();
            Console.WriteLine(
                $"Passenger created. " +
                $"Passenger ID: {passenger.PassengerID}");

            // =====================================
            // SEAT
            // =====================================

            Console.WriteLine();
            Console.WriteLine("SEAT SELECTION");

            Console.Write(
                "Enter seat number " +
                "(example: 12A): ");

            string seatNumber =
                Console.ReadLine()?
                .Trim()
                .ToUpper() ?? "";

            if (string.IsNullOrWhiteSpace(seatNumber))
            {
                Console.WriteLine(
                    "Seat number is required.");

                return;
            }

            bool seatAvailable =
                bookingService.IsSeatAvailable(
                    selectedFlight.FlightID,
                    seatNumber);

            if (!seatAvailable)
            {
                Console.WriteLine();
                Console.WriteLine(
                    "That seat is already booked " +
                    "for this flight.");

                return;
            }

            // =====================================
            // PAYMENT
            // =====================================

            Console.WriteLine();
            Console.WriteLine("PAYMENT");

            Console.WriteLine(
                $"Amount due: R{selectedFlight.Price:F2}");

            Console.WriteLine();
            Console.WriteLine("1. Card");
            Console.WriteLine("2. EFT");
            Console.WriteLine("3. Cash");

            Console.Write(
                "Select payment method: ");

            string paymentChoice =
                Console.ReadLine() ?? "";

            string paymentMethod;

            switch (paymentChoice)
            {
                case "1":
                    paymentMethod = "Card";
                    break;

                case "2":
                    paymentMethod = "EFT";
                    break;

                case "3":
                    paymentMethod = "Cash";
                    break;

                default:
                    Console.WriteLine(
                        "Invalid payment method.");

                    return;
            }

            // =====================================
            // CREATE PAYMENT
            // =====================================

            int paymentID =
                bookingService.CreatePayment(
                    selectedFlight.Price,
                    paymentMethod);

            // =====================================
            // CREATE BOOKING
            // =====================================

            int bookingID =
                bookingService.CreateBooking(
                    passenger.PassengerID,
                    selectedFlight.FlightID,
                    paymentID);

            // =====================================
            // CREATE TICKET
            // =====================================

            string ticketNumber =
                bookingService.CreateTicket(
                    bookingID,
                    seatNumber);

            // =====================================
            // CONFIRMATION
            // =====================================

            Console.WriteLine();
            Console.WriteLine(
                "=================================");
            Console.WriteLine(
                "       BOOKING CONFIRMED         ");
            Console.WriteLine(
                "=================================");

            Console.WriteLine(
                $"Booking ID: {bookingID}");

            Console.WriteLine(
                $"Passenger: " +
                $"{passenger.FirstName} " +
                $"{passenger.LastName}");

            Console.WriteLine(
                $"Flight ID: {selectedFlight.FlightID}");

            Console.WriteLine(
                $"Seat: {seatNumber}");

            Console.WriteLine(
                $"Payment ID: {paymentID}");

            Console.WriteLine(
                $"Payment Method: {paymentMethod}");

            Console.WriteLine(
                $"Ticket Number: {ticketNumber}");

            Console.WriteLine(
                $"Amount Paid: " +
                $"R{selectedFlight.Price:F2}");

            Console.WriteLine(
                "Status: Confirmed");

            Console.WriteLine(
                "=================================");
        }

        // =========================================
        // VIEW BOOKING
        // =========================================

        static void ViewBooking(
            BookingService bookingService)
        {
            Console.WriteLine("VIEW BOOKING");
            Console.WriteLine("============");

            Console.Write(
                "Enter Booking ID: ");

            if (!int.TryParse(
                Console.ReadLine(),
                out int bookingID))
            {
                Console.WriteLine(
                    "Invalid Booking ID.");

                return;
            }

            Booking? booking =
                bookingService.GetBookingByID(
                    bookingID);

            if (booking == null)
            {
                Console.WriteLine(
                    "Booking not found.");

                return;
            }

            Passenger? passenger =
                bookingService.GetPassengerByID(
                    booking.PassengerID);

            Console.WriteLine();
            Console.WriteLine("BOOKING DETAILS");
            Console.WriteLine("===============");

            Console.WriteLine(
                $"Booking ID: {booking.BookingID}");

            Console.WriteLine(
                $"Passenger ID: {booking.PassengerID}");

            if (passenger != null)
            {
                Console.WriteLine(
                    $"Passenger: " +
                    $"{passenger.FirstName} " +
                    $"{passenger.LastName}");

                Console.WriteLine(
                    $"Email: {passenger.Email}");

                Console.WriteLine(
                    $"Phone: {passenger.Phone}");
            }

            Console.WriteLine(
                $"Flight ID: {booking.FlightID}");

            Console.WriteLine(
                $"Booking Date: {booking.BookingDate}");

            Console.WriteLine(
                $"Payment ID: " +
                $"{booking.PaymentID?.ToString() ?? "None"}");

            Console.WriteLine(
                $"Status: {booking.BookingStatus}");
        }

        // =========================================
        // CANCEL BOOKING
        // =========================================

        static void CancelBooking(
            BookingService bookingService)
        {
            Console.WriteLine("CANCEL BOOKING");
            Console.WriteLine("==============");

            Console.Write(
                "Enter Booking ID: ");

            if (!int.TryParse(
                Console.ReadLine(),
                out int bookingID))
            {
                Console.WriteLine(
                    "Invalid Booking ID.");

                return;
            }

            Booking? booking =
                bookingService.GetBookingByID(
                    bookingID);

            if (booking == null)
            {
                Console.WriteLine(
                    "Booking not found.");

                return;
            }

            Console.WriteLine();
            Console.WriteLine(
                $"Current status: " +
                $"{booking.BookingStatus}");

            Console.Write(
                "Are you sure? (Y/N): ");

            string confirmation =
                Console.ReadLine()?
                .Trim()
                .ToUpper() ?? "";

            if (confirmation != "Y")
            {
                Console.WriteLine(
                    "Cancellation cancelled.");

                return;
            }

            bool cancelled =
                bookingService.CancelBooking(
                    bookingID);

            Console.WriteLine();

            if (cancelled)
            {
                Console.WriteLine(
                    "Booking cancelled successfully.");
            }
            else
            {
                Console.WriteLine(
                    "Booking could not be cancelled.");
            }
        }
    }
}