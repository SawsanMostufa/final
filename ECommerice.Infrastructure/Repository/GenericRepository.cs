using ECommerice.Core.Entities;
using ECommerice.Core.IRepository;
using ECommerice.Core.Specification;
using ECommerice.Infrastructure.SpecificationEvaluation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerice.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<int> GetCount()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task<IReadOnlyList<T>> GetLatestData(int take)
        {
            var result = await _context.Set<T>().Take(take).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<T>> GetAllPaging()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithInclude(string include)
        {
            return await _context.Set<T>()
                .Include(include)
                .ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAllWithIncludes(string include1, string include2)
        {
            return await _context.Set<T>()
                .Include(include1)
                .Include(include2)
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetWhereWithInclude(Expression<Func<T, bool>> predicate, string include)
        {
            return await _context.Set<T>()
                .Where(predicate)
                .Include(include)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetWherePagig(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>()
                .Where(predicate)
                .ToListAsync();
        }
        public async Task<T> GetWhereObject(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            var result =  await ApplySpecification(spec).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluation<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<bool> SaveChanges()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
