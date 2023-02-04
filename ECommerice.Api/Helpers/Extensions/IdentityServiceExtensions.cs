using ECommerice.Core.Entities;
using ECommerice.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerice.Api.Helpers.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration _config)
        {
            
            var builder = services.AddIdentityCore<AppUser>();

            builder = new IdentityBuilder(builder.UserType, builder.Services);
            builder.AddEntityFrameworkStores<StoreContext>();
            builder.AddSignInManager<SignInManager<AppUser>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"])),
                        ValidIssuer = _config["Token:Issuer"],
                        ValidateIssuer = true,
                        ValidateAudience = false
                    };
                });

            return services;
        }
    }
}
