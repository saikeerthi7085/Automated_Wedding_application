using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Automated_Wedding_Application.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Automated_Wedding_Application.Models;

namespace Automated_Wedding_Application.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<PlannerModel>().ToTable("Planner");
            builder.Entity<ServicesModel>().ToTable("plannerservices");
            builder.Entity<CustomerCart>().ToTable("CustomerCart");

            builder.Entity<PlannerModel>()
                .HasMany(t => t.services)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

        }

       public DbSet<PlannerModel> Planner { get; set; }
       public DbSet<ServicesModel> plannerservices { get; set; }

        public DbSet<CustomerCart> CustomerCarts { get; set; }

    }
}
