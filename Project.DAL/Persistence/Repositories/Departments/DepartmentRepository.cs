using Microsoft.EntityFrameworkCore;
using Project.DAL.Models.Departments;
using Project.DAL.Persistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Persistence.Repositories.Departments
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

     
        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Department> GetAll(bool WithAsNoTracking = true)
        {
            if (WithAsNoTracking)
            {
                return _dbContext.Departments.AsNoTracking().ToList();
            }
            return _dbContext.Departments.ToList();
        }
        public Department? GetById(int id)
        {
            //var department = _dbContext.Departments.Local.FirstOrDefault(D => D.Id == id);

            // if (department is null)
            // {
            //     department = _dbContext.Departments.FirstOrDefault(D => D.Id == id);

            // }
            // return department;
          //  return _dbContext.Find<Department>(id);
            return _dbContext.Departments.Find(id);
        }
        public int Add(Department entity)
        {
            _dbContext.Departments.Add(entity);
           return _dbContext.SaveChanges();
            
        }
        public int Update(Department entity)
        {
            _dbContext.Departments.Update(entity);
            return _dbContext.SaveChanges();
        }
        public int Delete(Department entity)
        {
            _dbContext.Departments.Remove(entity);
            return _dbContext.SaveChanges();
        }

      

       

      
    }
}
