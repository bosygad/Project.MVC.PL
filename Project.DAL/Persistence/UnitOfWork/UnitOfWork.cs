using Microsoft.EntityFrameworkCore;
using Project.DAL.Persistence.Data.Contexts;
using Project.DAL.Persistence.Repositories.Departments;
using Project.DAL.Persistence.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext) { 
        
          
            _dbContext = dbContext;
        }
        public IEmployeeRepository EmployeeRepository 
                => new EmployeeRepository(_dbContext);
            
        public IDepartmentRepository DepartmentRepository 
            =>  new DepartmentRepository(_dbContext);


    

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }
        public void Dispose() { 
        _dbContext.Dispose();
        }
    }
}
