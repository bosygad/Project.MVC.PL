using Project.DAL.Entities.Departments;
using Project.DAL.Persistence.Repositories._Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Persistence.Repositories.Departments
{
    public interface IDepartmentRepository : IGenericRepositroy<Department>
    {
       
    }
}
