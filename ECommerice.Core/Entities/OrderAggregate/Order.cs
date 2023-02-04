using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerice.Core.Entities.OrderAggregate
{
    public class Order :BaseEntity
    {
        public string UserId { get; set; }
        public string Username { get; set; }

        [Column(TypeName = "Date")]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
