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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<assignment>().HasKey(sc => new { sc.SoftwareId, sc.HardwareId, sc.UserId });
        }

        public DbSet<user> users { get; set; }

        public DbSet<hardware> hardware { get; set; }

        public DbSet<software> software { get; set; }

        public DbSet<assignment> assignment { get; set; }
    }
}
