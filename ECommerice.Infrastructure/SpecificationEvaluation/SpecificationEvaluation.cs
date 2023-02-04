using ECommerice.Core.Entities;
using ECommerice.Core.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerice.Infrastructure.SpecificationEvaluation
{
    public class SpecificationEvaluation<TEntitiy> where TEntitiy : BaseEntity
    {
        public static IQueryable<TEntitiy> GetQuery(IQueryable<TEntitiy> inputQuery,
            ISpecification<TEntitiy> spec)
        {
            var query = inputQuery;
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            if(spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if(spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            return query;
        }
    }
}
