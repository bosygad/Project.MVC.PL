using Microsoft.EntityFrameworkCore;
using Project.BLL.Models.Departments;
using Project.DAL.Entities.Departments;
using Project.DAL.Persistence.Data.Contexts;
using Project.DAL.Persistence.Repositories.Departments;
using Project.DAL.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        //        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(/*IDepartmentRepository departmentRepository*/ IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //  _departmentRepository = departmentRepository;
        }
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetIQueryable().Select(department => new DepartmentDto()
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                CreatedDate = department.CreatedDate,
            }).AsNoTracking().ToList();


            return departments;

        }

        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var departments = _unitOfWork.DepartmentRepository.GetById(id);
            if (departments is not null)
            {
                return new DepartmentDetailsDto()
                {
                    Id = departments.Id,
                    Code = departments.Code,
                    Name = departments.Name,
                    Description = departments.Description,
                    CreatedDate = departments.CreatedDate,
                    CreatedBy =1,
                    CreatedOn = DateTime.UtcNow,

                    LastModifiedBy = 1,
                   LastModifiedOn = DateTime.UtcNow,
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
                CreatedOn = DateTime.UtcNow,
                CreatedBy = 1,
                LastModifiedBy = 1,
              LastModifiedOn = DateTime.UtcNow,
            };
             _unitOfWork.DepartmentRepository.Add(department);
            return _unitOfWork.Complete();

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
             _unitOfWork.DepartmentRepository.Update(department);
            return _unitOfWork.Complete();

        }


        public bool DeleteDepartment(int id)
        {
            var departmentRepo = _unitOfWork.DepartmentRepository;
            var department = departmentRepo.GetById(id);
            if (department is { })
            {
                 departmentRepo.Delete(department) ;
            }
            return _unitOfWork.Complete() > 0;




        }
    }
}
