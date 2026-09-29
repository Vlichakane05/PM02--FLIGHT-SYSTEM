using System;
using System.Collections.Generic;
using System.Text;

namespace PM02__FLIGHT_SYSTEM.models
{
    public class Aircraft
    {
        public int AircraftID { get; set; }

        public string RegistrationNumber { get; set; } = "";

        public string Model { get; set; } = "";

        public int Capacity { get; set; }
    }
}
