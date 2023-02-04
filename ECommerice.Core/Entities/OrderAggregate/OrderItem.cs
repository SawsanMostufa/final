using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerice.Core.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        //public decimal Price { get; set; }
        //public int Quantity { get; set; }
        //public decimal? Total { get; set; }
        //public string PrductSize { get; set; }
        public virtual List<SizeItemorder> productSizes { get; set; }

        public int OrderId { get; set; }
    }
}
