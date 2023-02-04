using ECommerice.Core.Entities;
using ECommerice.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerice.Core.IUniteOfWork
{
    public interface IUniteOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}
