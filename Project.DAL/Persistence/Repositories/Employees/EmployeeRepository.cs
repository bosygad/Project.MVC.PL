using Project.DAL.Entities.Empeloyees;
using Project.DAL.Persistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.DAL.Persistence.Repositories._Generic;

namespace Project.DAL.Persistence.Repositories.Employees
{
    public class EmployeeRepository : GenericRepositroy<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
