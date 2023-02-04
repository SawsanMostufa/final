using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerice.Api.DTOs
{
    public class UsersDTO
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
