using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public static class SpecificationEvaluator <T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> InputQuery, ISpecifications<T> Spec)                           
        {
            var Query = InputQuery; // DbContext.Set<T>

            if (Spec.Criteria is not null)
            {
                Query=Query.Where(Spec.Criteria);
            }


            Query = Spec.Includes.Aggregate(Query, (CurQ, IncludeExpression) => (CurQ.Include(IncludeExpression)));

            return Query;
        }


    }
}
  