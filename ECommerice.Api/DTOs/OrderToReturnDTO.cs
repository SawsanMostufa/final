using ECommerice.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerice.Api.DTOs
{
    public class OrderToReturnDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
