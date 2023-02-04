using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerice.Api.DTOs
{
    public class CommonSearchDTO
    {
        public int PageNo { get; set; } = 1;
        public int pageSize { get; set; } = 5;
        public string SearchText { get; set; }
    }
}
