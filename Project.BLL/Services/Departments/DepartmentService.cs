using Microsoft.EntityFrameworkCore;
using Project.BLL.Models.Departments;
using Project.DAL.Entities.Departments;
using Project.DAL.Persistence.Data.Contexts;
using Project.DAL.Persistence.Repositories.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public IEnumerable<DepartmentToReturnDto> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAllAsIQueryable().Select(department => new DepartmentToReturnDto()
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                CreatedDate = department.CreatedDate,
            }).AsNoTracking().ToList();


            return departments;

        }

        public DepartmentDetailsToReutrnDto? GetDepartmentById(int id)
        {
            var departments = _departmentRepository.GetById(id);
            if (departments is not null)
            {
                return new DepartmentDetailsToReutrnDto()
                {
                    Id = departments.Id,
                    Code = departments.Code,
                    Name = departments.Name,
                    Description = departments.Description,
                    CreatedDate = departments.CreatedDate,
                    CreatedBy = departments.CreatedBy,
                    CreatedOn = departments.CreatedOn,

                    LastModifiedBy = departments.LastModifiedBy,
                    LastModifiedOn = departments.LastModifiedOn,
                };
            }
            return null;
        }
        public int CreateDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = new Department()
            {
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreatedDate = departmentDto.CreatedDate,
                //CreatedOn = DateTime.UtcNow,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };
            return _departmentRepository.Add(department);

        }

        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            var department = new Department()
            {
                Id = departmentDto.Id,
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreatedDate = departmentDto.CreatedDate,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };
            return _departmentRepository.Update(department);

        }


        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department is { })
            {
                return _departmentRepository.Delete(department) > 0;
            }
            return false;




        }
    }
}
