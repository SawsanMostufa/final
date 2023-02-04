using ECommerice.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerice.Core.Specification
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParam productParams)
            :base(x => 
                (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
                  (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
                 (!productParams.CategoryId.HasValue || x.CategoryId == productParams.CategoryId))
        {
            
            AddInclude(p => p.ProductSizes);
            AddOrderBy(x => x.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            //if (!string.IsNullOrEmpty(productParams.Sort))
            //{
            //    switch (productParams.Sort)
            //    {
            //        case "priceAsc":
            //            AddOrderBy(p => p.Price);
            //            break;
            //        case "priceDesc":
            //            AddOrderByDescending(p => p.Price);
            //            break;
            //        default:
            //            AddOrderBy(p => p.Name);
            //            break;
            //    }
            //}
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.Category);
            AddInclude(p => p.ProductSizes);
        }

    }
}
