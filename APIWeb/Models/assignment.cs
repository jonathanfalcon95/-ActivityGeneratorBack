using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace APIWeb.Models
{
    public class assignment
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public user users { get; set; }
        public long HardwareId { get; set; }
        public hardware hardware { get; set; }
        public long SoftwareId { get; set; }
        public software software { get; set; }

    }
}
