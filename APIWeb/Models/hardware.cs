﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace APIWeb.Models
{
    public class hardware
    {
        public long Id { get; set; }
        public string HardwareName { get; set; }

    }
}
