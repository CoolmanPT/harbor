using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirMonit_Service.Models
{
    public class WeekDay
    {
        public int DayNr { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public float Avg { get; set; }
    }
}