using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerice.Api.Helpers.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string RetrieveEmailFromClaimPrincipal(this ClaimsPrincipal user)
        {
            //return user?.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Email).Value;
            return user.FindFirstValue(ClaimTypes.Email);
        }
    }
}
