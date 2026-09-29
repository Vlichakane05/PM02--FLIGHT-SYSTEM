using System;
using System.Collections.Generic;
using System.Text;

namespace PM02__FLIGHT_SYSTEM.Models
{
    public class Flight
    {
        public int FlightID { get; set; }

        public int DepartureAirportID { get; set; }

        public int ArrivalAirportID { get; set; }

        public DateTime DepartureDateTime { get; set; }

        public DateTime ArrivalDateTime { get; set; }

        public decimal Price { get; set; }

        public string Status { get; set; } = "Scheduled";
    }
}