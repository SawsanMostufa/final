using ECommerice.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerice.Api.DTOs
{
    public class UserDTO
    {
        public AppUser UserData { get; set; } = new AppUser();
        public string Email { get; set; }
        public string Displayname { get; set; }
        public string Token { get; set; }
    }

   
}
