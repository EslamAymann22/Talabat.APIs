using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        StoreContext _DbContext;
        public GenericRepository(StoreContext DbContext) {
            _DbContext = DbContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            //return (Task<IEnumerable<T>>)_DbContext.Set<T>().ToList();
            if (typeof(T) == typeof(Product))
            {
                return (IEnumerable<T>)await _DbContext.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).ToListAsync();
            }
            return await  _DbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            //=> await _DbContext.Set<T>().Where(X => X.Id == id).FirstOrDefaultAsync();
            //if (typeof(T) == typeof(Product))
            //{
            //    return  await _DbContext.Products.Include(p => p.ProductBrand).Include(p => p.ProductType)
            //        .Where(P => P.Id == id).FirstOrDefault();

            //}
            return await _DbContext.Set<T>().FindAsync(id);
        }


    }
}
