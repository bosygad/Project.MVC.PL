using Microsoft.EntityFrameworkCore;
using Project.DAL.Entities.Departments;
using Project.DAL.Persistence.Data.Contexts;
using Project.DAL.Persistence.Repositories._Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Persistence.Repositories.Departments
{
    public class DepartmentRepository : GenericRepositroy<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

       
         
    }
}
