﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirMonit_Service.Models
{
    public class Hour
    {
        public int HourNr { get; set; }

        public int Min { get; set; }

        public float Avg { get; set; }

        public int Max { get; set; }
    }
}