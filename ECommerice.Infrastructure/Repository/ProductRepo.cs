using ECommerice.Core.Entities;
using ECommerice.Core.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerice.Infrastructure.Repository
{
    public class ProductRepo : IProductRepo
    {
        public readonly StoreContext _context;

        public ProductRepo(StoreContext context)
        {
            _context = context;
        }        

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Product
                .Include(p => p.Category)
                .Include(p=>p.ProductSizes)
                .ToListAsync();
        }

        public async Task<Product> GetProductsByIdAsync(int id)
        {
            return await _context.Product
                .Include(p => p.Category)
                .Include(p => p.ProductSizes)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
       

        public async Task<IReadOnlyList<Category>> GetCategoryAsync()
        {
            return await _context.Category.ToListAsync();
        }
    }
}
