using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ECommerice.Core.Entities;
using ECommerice.Core.Specification;

namespace ECommerice.Core.IRepository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<int> GetCount();
        Task<IReadOnlyList<T>> GetLatestData(int take);
        Task<IEnumerable<T>> GetAllPaging();
        Task<IEnumerable<T>> GetWherePagig(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllWithInclude(string include);
        Task<IEnumerable<T>> GetAllWithIncludes(string include1, string include2);
        Task<T> GetWhereObject(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetWhereWithInclude(Expression<Func<T, bool>> predicate, string include);
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> SaveChanges();
    }
}