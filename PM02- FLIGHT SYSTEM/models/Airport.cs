using System;
using System.Collections.Generic;
using System.Text;

namespace PM02__FLIGHT_SYSTEM.Models
{
    public class Airport
    {
        public int AirportID { get; set; }

        public string AirportCode { get; set; } = "";

        public string AirportName { get; set; } = "";

        public string City { get; set; } = "";

        public string Country { get; set; } = "";
    }
}