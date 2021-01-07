using DataLayer.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIJWTLayerProj.Config.Extensions
{
    public static class IdentityExtension
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddDbContext<MyContext>();
            services.AddIdentity<IdentityUser, IdentityRole>().
                AddEntityFrameworkStores<MyContext>().
                AddDefaultTokenProviders();
            return services;
        }
    }
}
