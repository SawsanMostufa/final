using ECommerice.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerice.Api.DTOs
{
    public class AdminUserDTO
    {
        public AppUser UserData { get; set; }
        public string Displayname { get; set; }
        public string Token { get; set; }
    }
}
