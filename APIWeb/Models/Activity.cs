﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIWeb.Models
{
    public class Activity
    {
        public long activId { get; set; }
        public string subjet { get; set; }
        public string description { get; set; }
        public long levelId { get; set; }
    }
}
