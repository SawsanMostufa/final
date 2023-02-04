using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerice.Api.DTOs
{
    public class CustomerBasketDTO
    {
        public string Id { get; set; }
        public List<BasketItemsDTO> Items { get; set; }
    }
}
