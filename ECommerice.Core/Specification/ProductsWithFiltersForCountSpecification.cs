using ECommerice.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerice.Core.Specification
{
    public class ProductsWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductsWithFiltersForCountSpecification(ProductSpecParam productParams)
             : base(x =>
                 (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
                 (!productParams.CategoryId.HasValue || x.CategoryId == productParams.CategoryId))
        {

        }
    }
}
