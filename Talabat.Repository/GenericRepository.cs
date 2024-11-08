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

        public Task<IEnumerable<T>> GetAllAsync()
        {

            return (Task<IEnumerable<T>>)_DbContext.Set<T>().ToList();

            //throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
