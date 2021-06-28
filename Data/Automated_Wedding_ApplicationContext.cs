using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Automated_Wedding_Application.Models;

namespace Automated_Wedding_Application.Data
{
    public class Automated_Wedding_ApplicationContext : DbContext
    {
        public Automated_Wedding_ApplicationContext (DbContextOptions<Automated_Wedding_ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Automated_Wedding_Application.Models.PlannerModel> PlannerModel { get; set; }

        public DbSet<Automated_Wedding_Application.Models.ServicesModel> ServicesModel { get; set; }

        public DbSet<Automated_Wedding_Application.Models.CustomerCart> CustomerCart { get; set; }
    }
}
