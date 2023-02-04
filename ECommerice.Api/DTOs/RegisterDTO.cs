using ECommerice.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerice.Api.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string Displayname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",ErrorMessage =
            "Password must have 1 uppercase, 1 lowercase, 1 number, 1 non alphanumeric and at least 6 char")]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }

        public AddressDTO Address { get; set; }

    }
}
