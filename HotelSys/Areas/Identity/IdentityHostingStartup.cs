using System;
using HotelSys.Areas.Identity.Data;
using HotelSys.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(HotelSys.Areas.Identity.IdentityHostingStartup))]
namespace HotelSys.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<HotelSysContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("Hotel_alkheerContext")));

                services.AddDefaultIdentity<IdentityUser>(
                  options =>
                  {
                      options.SignIn.RequireConfirmedAccount = false;
                      options.Password.RequireUppercase = false;
                      options.Password.RequireLowercase = false;
                      options.Password.RequireDigit = false;
                      options.Password.RequiredUniqueChars = 0;
                      options.Password.RequireNonAlphanumeric = false;
                      options.Password.RequiredLength = 0;

                     // options.Password.RequireLowercase
                  }

                  )
                  .AddEntityFrameworkStores<HotelSysContext>();

                //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddEntityFrameworkStores<HotelSysContext>();
            });
        }
    }
}