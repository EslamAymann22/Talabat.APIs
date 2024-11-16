using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        // without Generic include 
        Task<IReadOnlyList<T>> GetAllAsync(); 
        Task<T> GetByIdAsync(int id);
        // without Generic include 



        //// with Generic include  
        
        Task<IReadOnlyList<T>> GetAllAsyncGeneric(ISpecifications<T> spe);
        Task<T> GetByIdAsyncGeneric(ISpecifications<T> spe);


        //// with Generic include  
    }
}
