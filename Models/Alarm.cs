using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirMonit_Service.Models
{
    public class Alarm
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string Date { get; set; }
        public string City { get; set; }
        public string Message { get; set; }
        public string Time { get; set; }
    }
}