using ECommerice.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerice.Api.DTOs
{
    public class OrderDTO
    {
        public List<OrderItem> OrderItems { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDTO ShipToAddress { get; set; }
    }
}
