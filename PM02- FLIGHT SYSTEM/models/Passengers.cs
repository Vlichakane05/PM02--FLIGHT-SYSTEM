using System;
using System.Collections.Generic;
using System.Text;

namespace PM02__FLIGHT_SYSTEM.Models
{
    public class Passenger
    {
        public int PassengerID { get; set; }

        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public string Email { get; set; } = "";

        public string Phone { get; set; } = "";

        public string PassportNumber { get; set; } = "";
    }
}