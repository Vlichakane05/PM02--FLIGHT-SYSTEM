using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
namespace PM02__FLIGHT_SYSTEM.Models
{
    public class Booking
    {
        public int BookingID { get; set; }

        public int PassengerID { get; set; }

        public int FlightID { get; set; }

        public DateTime BookingDate { get; set; }

        public int? PaymentID { get; set; }

        public string BookingStatus { get; set; } = "Pending";
    }
}