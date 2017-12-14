using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirMonit_Service.Models
{
    public class Hour
    {
        public int hour { get; set; }

        public int min { get; set; }

        public float avg { get; set; }

        public int max { get; set; }
    }
}