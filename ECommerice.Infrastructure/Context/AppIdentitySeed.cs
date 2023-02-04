using ECommerice.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerice.Infrastructure.Context
{
    public class AppIdentitySeed
    {
        public static async Task SeedUsersAsyc(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "admin",
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com",
                    UserType = "admin",
                    Address = new Address
                    {
                        FirstName = "admin",
                        LastName = "admin",
                        Street = "10 the street",
                        City = "Sohage",
                        State = "Egypt",
                        ZipCode = "71111"
                    }
                };
                await userManager.CreateAsync(user, "SSr!nwm08");
            }
        }
    }
}
