using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirMonit_Service.Models
{
    public class Parameter
    {
        public string Name { get; set; }
        public int Value   { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string City { get; set; }

    }
}