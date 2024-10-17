using Microsoft.EntityFrameworkCore;
using Project.DAL.Entities;
using Project.DAL.Persistence.Data.Contexts;

namespace Project.DAL.Persistence.Repositories._Generic
{
    public class GenericRepositroy<T> : IGenericRepositroy<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _dbContext;


        public GenericRepositroy(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool WithAsNoTracking = true)
        {
            if (WithAsNoTracking)
            {
                return await _dbContext.Set<T>().Where(X => !X.IsDeleted).AsNoTracking().ToListAsync();
            }
            return await _dbContext.Set<T>().Where(X => !X.IsDeleted).ToListAsync();
        }
        public IQueryable<T> GetIQueryable()
        {
            return _dbContext.Set<T>();
        }
        public IEnumerable<T> GetIEnumerable()
        {
            return _dbContext.Set<T>();
        }
        public async Task<T?> GetByIdAsync(int id)
        {
      
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public void Add(T entity)
        =>  _dbContext.Set<T>().Add(entity);
           

        
        public void Update(T entity)
       => _dbContext.Set<T>().Update(entity);
          
        
        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            _dbContext.Set<T>().Update(entity);
           
        }

       
    }
}
