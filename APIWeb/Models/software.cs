using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIWeb.Models
{
    public class software
    {
        [Key]
        [ForeignKey("software")]
        public long Id { get; set; }
        public string SoftwareName { get; set; }

    }
}
