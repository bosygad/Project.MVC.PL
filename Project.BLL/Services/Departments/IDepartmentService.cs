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
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
      Task<DepartmentDetailsDto?> GetDepartmentByIdAsync(int id);
        Task<int> CreateDepartmentAsync(CreatedDepartmentDto departmentDto);
        Task<int> UpdateDepartmentAsync (UpdatedDepartmentDto departmentDto);
       Task <bool> DeleteDepartmentAsync(int id);

    }
}
