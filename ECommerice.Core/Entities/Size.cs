using ECommerice.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerice.Core.Entities
{
    public class Size : BaseEntity
    {
        public string value { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
       
        public int Quantity { get; set; }
     
        //public int basketItemId { get; set; }
        public int ProductId { get; set; }
 
        //public int? OrderItemId { get; set; }
    }
}
