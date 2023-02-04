using ECommerice.Core.Entities;
using ECommerice.Core.IRepository;
using ECommerice.Core.IUniteOfWork;
using ECommerice.Infrastructure.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerice.Infrastructure.UniteOfWork
{
    public class UniteOfWork : IUniteOfWork
    {
        public readonly StoreContext _context;
        public Hashtable _repositories;

        public UniteOfWork(StoreContext context)
        {
            _context = context;
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null) _repositories = new Hashtable();
            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);
            }
            return (IGenericRepository<TEntity>)_repositories[type];
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
