using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerice.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        //public int Quantity { get; set; }
        public string PictureUrl { get; set; }
        public DateTime CreatedDate { get; set; }

        //Relations
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public virtual List<Size> ProductSizes { get; set; }


    }
}