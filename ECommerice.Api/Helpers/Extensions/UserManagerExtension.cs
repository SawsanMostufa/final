using ECommerice.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerice.Api.Helpers.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<AppUser> FindByUserClaimPrincipleWithAddressAsync(this UserManager<AppUser> input,
            ClaimsPrincipal user)
        {
            var email = user.
                Claims?
                .FirstOrDefault(u => u.Type == ClaimTypes.Email)?
                .Value;

            return await input
                .Users
                .Include(u => u.Address)
                .SingleOrDefaultAsync(x => x.Email == email);
        }

        public static async Task<AppUser> FindByEmailFromClaimPrincipleAsync(this UserManager<AppUser> input,
           ClaimsPrincipal user)
        {
            var email = user.
                Claims?
                .FirstOrDefault(u => u.Type == ClaimTypes.Email)?
                .Value;

            return await input
                .Users
                .Include(u => u.Address)
                .SingleOrDefaultAsync(x => x.Email == email);
        }
    }
}
