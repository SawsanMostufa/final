using ECommerice.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerice.Core.IRepository
{
    public interface IProductRepo
    {
        Task<Product> GetProductsByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<IReadOnlyList<Category>> GetCategoryAsync();
    }
}
