using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerice.Api.DTOs
{
    public class OrderSearchDTO
    {
        public int PageNo { get; set; } = 1;
        public int pageSize { get; set; } = 5;
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
