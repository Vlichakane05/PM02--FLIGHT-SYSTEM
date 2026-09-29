using Microsoft.Data.SqlClient;
using PM02__FLIGHT_SYSTEM.Data;
using PM02__FLIGHT_SYSTEM.Models;

namespace PM02__FLIGHT_SYSTEM.Services
{
    public class DatabaseService
    {
        private readonly DatabaseConnection databaseConnection;

        public DatabaseService()
        {
            databaseConnection = new DatabaseConnection();
        }

        // =========================================
        // GET ALL FLIGHTS
        // =========================================

        public List<Flight> GetFlights()
        {
            List<Flight> flights = new List<Flight>();

            using (SqlConnection connection =
                databaseConnection.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT
                        FlightID,
                        DepartureAirportID,
                        ArrivalAirportID,
                        DepartureDateTime,
                        ArrivalDateTime,
                        Price,
                        Status
                    FROM Flights
                    ORDER BY DepartureDateTime";

                using (SqlCommand command =
                    new SqlCommand(query, connection))
                using (SqlDataReader reader =
                    command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Flight flight = new Flight
                        {
                            FlightID =
                                Convert.ToInt32(
                                    reader["FlightID"]),

                            DepartureAirportID =
                                Convert.ToInt32(
                                    reader["DepartureAirportID"]),

                            ArrivalAirportID =
                                Convert.ToInt32(
                                    reader["ArrivalAirportID"]),

                            DepartureDateTime =
                                Convert.ToDateTime(
                                    reader["DepartureDateTime"]),

                            ArrivalDateTime =
                                Convert.ToDateTime(
                                    reader["ArrivalDateTime"]),

                            Price =
                                Convert.ToDecimal(
                                    reader["Price"]),

                            Status =
                                reader["Status"]?.ToString()
                                ?? ""
                        };

                        flights.Add(flight);
                    }
                }
            }

            return flights;
        }

        // =========================================
        // GET AIRPORT
        // =========================================

        public Airport? GetAirportByID(int airportID)
        {
            using (SqlConnection connection =
                databaseConnection.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT
                        AirportID,
                        AirportCode,
                        AirportName,
                        City,
                        Country
                    FROM Airports
                    WHERE AirportID = @AirportID";

                using (SqlCommand command =
                    new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@AirportID",
                        airportID);

                    using (SqlDataReader reader =
                        command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Airport
                            {
                                AirportID =
                                    Convert.ToInt32(
                                        reader["AirportID"]),

                                AirportCode =
                                    reader["AirportCode"]?.ToString()
                                    ?? "",

                                AirportName =
                                    reader["AirportName"]?.ToString()
                                    ?? "",

                                City =
                                    reader["City"]?.ToString()
                                    ?? "",

                                Country =
                                    reader["Country"]?.ToString()
                                    ?? ""
                            };
                        }
                    }
                }
            }

            return null;
        }

        // =========================================
        // GET PASSENGER
        // =========================================

        public Passenger? GetPassengerByID(int passengerID)
        {
            using (SqlConnection connection =
                databaseConnection.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT
                        PassengerID,
                        FirstName,
                        LastName,
                        Email,
                        Phone,
                        PassportNumber
                    FROM Passengers
                    WHERE PassengerID = @PassengerID";

                using (SqlCommand command =
                    new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@PassengerID",
                        passengerID);

                    using (SqlDataReader reader =
                        command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Passenger
                            {
                                PassengerID =
                                    Convert.ToInt32(
                                        reader["PassengerID"]),

                                FirstName =
                                    reader["FirstName"]?.ToString()
                                    ?? "",

                                LastName =
                                    reader["LastName"]?.ToString()
                                    ?? "",

                                Email =
                                    reader["Email"]?.ToString()
                                    ?? "",

                                Phone =
                                    reader["Phone"]?.ToString()
                                    ?? "",

                                PassportNumber =
                                    reader["PassportNumber"]?.ToString()
                                    ?? ""
                            };
                        }
                    }
                }
            }

            return null;
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
            using (SqlConnection connection =
                databaseConnection.GetConnection())
            {
                connection.Open();

                string query = @"
                    INSERT INTO Passengers
                    (
                        FirstName,
                        LastName,
                        Email,
                        Phone,
                        PassportNumber
                    )
                    OUTPUT INSERTED.PassengerID
                    VALUES
                    (
                        @FirstName,
                        @LastName,
                        @Email,
                        @Phone,
                        @PassportNumber
                    )";

                using (SqlCommand command =
                    new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@FirstName",
                        firstName);

                    command.Parameters.AddWithValue(
                        "@LastName",
                        lastName);

                    command.Parameters.AddWithValue(
                        "@Email",
                        email);

                    command.Parameters.AddWithValue(
                        "@Phone",
                        phone);

                    command.Parameters.AddWithValue(
                        "@PassportNumber",
                        passportNumber);

                    int passengerID =
                        Convert.ToInt32(
                            command.ExecuteScalar());

                    return new Passenger
                    {
                        PassengerID = passengerID,
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        Phone = phone,
                        PassportNumber = passportNumber
                    };
                }
            }
        }

        // =========================================
        // CHECK SEAT AVAILABILITY
        // =========================================

        public bool IsSeatAvailable(
            int flightID,
            string seatNumber)
        {
            using (SqlConnection connection =
                databaseConnection.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT COUNT(*)
                    FROM Tickets t
                    INNER JOIN Bookings b
                        ON t.BookingID = b.BookingID
                    WHERE b.FlightID = @FlightID
                    AND t.SeatNumber = @SeatNumber
                    AND t.TicketStatus = 'Active'
                    AND b.BookingStatus <> 'Cancelled'";

                using (SqlCommand command =
                    new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@FlightID",
                        flightID);

                    command.Parameters.AddWithValue(
                        "@SeatNumber",
                        seatNumber);

                    int count =
                        Convert.ToInt32(
                            command.ExecuteScalar());

                    return count == 0;
                }
            }
        }

        // =========================================
        // CREATE PAYMENT
        // =========================================

        public int CreatePayment(
            decimal amount,
            string paymentMethod)
        {
            using (SqlConnection connection =
                databaseConnection.GetConnection())
            {
                connection.Open();

                string query = @"
                    INSERT INTO Payments
                    (
                        Amount,
                        PaymentDate,
                        PaymentMethod,
                        PaymentStatus
                    )
                    OUTPUT INSERTED.PaymentID
                    VALUES
                    (
                        @Amount,
                        GETDATE(),
                        @PaymentMethod,
                        'Paid'
                    )";

                using (SqlCommand command =
                    new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@Amount",
                        amount);

                    command.Parameters.AddWithValue(
                        "@PaymentMethod",
                        paymentMethod);

                    return Convert.ToInt32(
                        command.ExecuteScalar());
                }
            }
        }

        // =========================================
        // CREATE BOOKING
        // =========================================

        public int CreateBooking(
            int passengerID,
            int flightID,
            int paymentID)
        {
            using (SqlConnection connection =
                databaseConnection.GetConnection())
            {
                connection.Open();

                string query = @"
                    INSERT INTO Bookings
                    (
                        PassengerID,
                        FlightID,
                        BookingDate,
                        PaymentID,
                        BookingStatus
                    )
                    OUTPUT INSERTED.BookingID
                    VALUES
                    (
                        @PassengerID,
                        @FlightID,
                        GETDATE(),
                        @PaymentID,
                        'Confirmed'
                    )";

                using (SqlCommand command =
                    new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@PassengerID",
                        passengerID);

                    command.Parameters.AddWithValue(
                        "@FlightID",
                        flightID);

                    command.Parameters.AddWithValue(
                        "@PaymentID",
                        paymentID);

                    return Convert.ToInt32(
                        command.ExecuteScalar());
                }
            }
        }

        // =========================================
        // CREATE TICKET
        // =========================================

        public string CreateTicket(
            int bookingID,
            string seatNumber)
        {
            using (SqlConnection connection =
                databaseConnection.GetConnection())
            {
                connection.Open();

                string ticketNumber =
                    "TKT" +
                    DateTime.Now.ToString("yyyyMMddHHmmss");

                string query = @"
                    INSERT INTO Tickets
                    (
                        TicketNumber,
                        BookingID,
                        SeatNumber,
                        TicketStatus
                    )
                    VALUES
                    (
                        @TicketNumber,
                        @BookingID,
                        @SeatNumber,
                        'Active'
                    )";

                using (SqlCommand command =
                    new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@TicketNumber",
                        ticketNumber);

                    command.Parameters.AddWithValue(
                        "@BookingID",
                        bookingID);

                    command.Parameters.AddWithValue(
                        "@SeatNumber",
                        seatNumber);

                    command.ExecuteNonQuery();
                }

                return ticketNumber;
            }
        }

        // =========================================
        // GET BOOKING
        // =========================================

        public Booking? GetBookingByID(int bookingID)
        {
            using (SqlConnection connection =
                databaseConnection.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT
                        BookingID,
                        PassengerID,
                        FlightID,
                        BookingDate,
                        PaymentID,
                        BookingStatus
                    FROM Bookings
                    WHERE BookingID = @BookingID";

                using (SqlCommand command =
                    new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@BookingID",
                        bookingID);

                    using (SqlDataReader reader =
                        command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Booking
                            {
                                BookingID =
                                    Convert.ToInt32(
                                        reader["BookingID"]),

                                PassengerID =
                                    Convert.ToInt32(
                                        reader["PassengerID"]),

                                FlightID =
                                    Convert.ToInt32(
                                        reader["FlightID"]),

                                BookingDate =
                                    Convert.ToDateTime(
                                        reader["BookingDate"]),

                                PaymentID =
                                    reader["PaymentID"] == DBNull.Value
                                        ? null
                                        : Convert.ToInt32(
                                            reader["PaymentID"]),

                                BookingStatus =
                                    reader["BookingStatus"]?
                                    .ToString() ?? ""
                            };
                        }
                    }
                }
            }

            return null;
        }

        // =========================================
        // CANCEL BOOKING
        // =========================================

        public bool CancelBooking(int bookingID)
        {
            using (SqlConnection connection =
                databaseConnection.GetConnection())
            {
                connection.Open();

                string query = @"
                    UPDATE Bookings
                    SET BookingStatus = 'Cancelled'
                    WHERE BookingID = @BookingID
                    AND BookingStatus <> 'Cancelled'";

                using (SqlCommand command =
                    new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@BookingID",
                        bookingID);

                    int rowsAffected =
                        command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }
    }
}