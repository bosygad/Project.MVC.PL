using Project.BLL.Models.Departments;
using Project.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Services.Departments
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentToReturnDto> GetAllDepartments();
      DepartmentDetailsToReutrnDto? GetDepartmentById(int id);
        int CreateDepartment(CreatedDepartmentDto departmentDto);
        int UpdateDepartment (UpdatedDepartmentDto departmentDto);
        bool DeleteDepartment(int id);

    }
}
