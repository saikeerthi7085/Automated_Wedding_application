using System;
using Automated_Wedding_Application.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Automated_Wedding_Application.Areas.Identity.Data;

[assembly: HostingStartup(typeof(Automated_Wedding_Application.Areas.Identity.IdentityHostingStartup))]
namespace Automated_Wedding_Application.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ApplicationDbContext>(options =>
                   options.UseSqlServer(
                       context.Configuration.GetConnectionString("DefaultConnection")));

                services.AddIdentityCore<ApplicationUser>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultUI().AddDefaultTokenProviders();
                
            });


            
        }
    }
}