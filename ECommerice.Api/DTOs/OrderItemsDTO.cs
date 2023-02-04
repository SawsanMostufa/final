using ECommerice.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerice.Api.DTOs
{
    public class OrderItemsDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        //public List<ProductSizeDto> productsizes { get; set; }
        public virtual List<ItemSizeDTO> productSizes { get; set; }

        public static explicit operator List<object>(OrderItemsDTO v)
        {
            throw new NotImplementedException();
        }
        //public decimal Price { get; set; }
        //public int Quantity { get; set; }
        //public string size { get; set; }
    }
    public class ItemSizeDTO
    {
        public int Id { get; set; }
        public string value { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int ProductId { get; set; }
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
    }
}
