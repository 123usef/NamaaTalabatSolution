using Microsoft.EntityFrameworkCore;
using NamaaTalabat.Core.Entities;
using NamaaTalabat.Core.Repositories.Contract;
using NamaaTalabat.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamaaTalabat.Repository
{
    //[Authenticate("admin","superadmin")]
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<T?> Get(int id)
        //{   if(typeof(T) == typeof(Product))
        //return await _dbContext.Set<Product>().Include(p => p.Category).Include(p => p.Brand).FirstOrDefault(id);
        { 
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Product))
                return (IEnumerable<T>) await _dbContext.Set<Product>().Include(p => p.Category).Include(p => p.Brand).ToListAsync();
            return (IEnumerable<T>) await _dbContext.Set<T>().ToListAsync();
        }
    }
}
