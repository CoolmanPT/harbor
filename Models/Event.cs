using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirMonit_Service.Models
{
    public class Event
    {
        public string User_id { get; set; }
        public string Eventh { get; set; }
        public float Temperature { get; set; }
        public string City { get; set; }
        public DateTime Date { get; set; }
    }
}