using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerice.Api.Helpers
{
    public class Pagination <T> where T: class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}
