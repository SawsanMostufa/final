using ECommerice.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerice.Api.DTOs
{
    public class ProductToReturnDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public int  Quantity{ get; set; }
      
        public string PictureUrl { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public virtual List<ProductSizeDto> ProductSizes { get; set; }
    }

    public class ProductSizeDto
    {
        public int Id { get; set; }
        public string value { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int ProductId { get; set; }
        //public int? OrderItemId { get; set; }
        public int Quantity { get; set; }
    }
}
