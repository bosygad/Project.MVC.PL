using Microsoft.EntityFrameworkCore;
using Project.DAL.Entities;
using Project.DAL.Persistence.Data.Contexts;

namespace Project.DAL.Persistence.Repositories._Generic
{
    public class GenericRepositroy<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _dbContext;


        public GenericRepositroy(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<T> GetAll(bool WithAsNoTracking = true)
        {
            if (WithAsNoTracking)
            {
                return _dbContext.Set<T>().Where(X => !X.IsDeleted).AsNoTracking().ToList();
            }
            return _dbContext.Set<T>().Where(X => !X.IsDeleted).ToList();
        }
        public IQueryable<T> GetAllAsIQueryable()
        {
            return _dbContext.Set<T>();
        }
        public T? GetById(int id)
        {
            //var T = _dbContext.Ts.Local.FirstOrDefault(D => D.Id == id);

            // if (T is null)
            // {
            //     T = _dbContext.Ts.FirstOrDefault(D => D.Id == id);

            // }
            // return T;
            //  return _dbContext.Find<T>(id);
            return _dbContext.Set<T>().Find(id);
        }
        public int Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return _dbContext.SaveChanges();

        }
        public int Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }
        public int Delete(T entity)
        {
            entity.IsDeleted = true;
            _dbContext.Set<T>().Remove(entity);
            return _dbContext.SaveChanges();
        }
    }
}
