using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace APIWeb.Models
{
    public class Technology
    {
        [Key]
        [ForeignKey("Technology")]
        public long techId { get; set; }
        public string techName { get; set; }
        public List<Technology> AllTechnology { get; set; }
    }
}
