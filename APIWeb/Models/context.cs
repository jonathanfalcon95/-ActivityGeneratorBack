using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace APIWeb.Models
{
    public class context : DbContext
    {
        public context(DbContextOptions<context> options)
          : base(options)
        {
        }
        public DbSet<Activity>  Activities { get; set; }

        public DbSet<user> users { get; set; }

        public DbSet<Level> level { get; set; }

        public DbSet<ActivTech>  ActivTech { get; set; }
    }
}
