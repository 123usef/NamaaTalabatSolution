using Microsoft.EntityFrameworkCore;
using NamaaTalabat.Core.Entities;
using NamaaTalabat.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamaaTalabat.Repository
{
    public static class SpecififcationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> BuidQuery(IQueryable<T> initial , ISpecification<T> spec)
        {
            var query = initial;
            if (spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }
               //query = spec.Includes.Aggregate
            //(query, (currentQuery, includeExpression)
            //=> currentQuery.Include(includeExpression));

            query = spec.Includes.Aggregate(query, (currentQuery, IncludeExpression)
                => currentQuery.Include(IncludeExpression));

            return query; 
        }
    }
}
