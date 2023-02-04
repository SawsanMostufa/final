using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerice.Api.DTOs
{
    public class UserProfileDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Displayname { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string PhoneNumber { get; set; }

        public AddressDTO Address { get; set; }
    }
}
