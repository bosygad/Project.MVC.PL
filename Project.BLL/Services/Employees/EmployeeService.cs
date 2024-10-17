using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project.BLL.Common.Services.Attachments;
using Project.BLL.Models.Employees;
using Project.DAL.Entities.Empeloyees;
using Project.DAL.Persistence.Repositories.Employees;
using Project.DAL.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentService _attachmentService;

        //private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(/*IEmployeeRepository employeeRepository*/
            IUnitOfWork unitOfWork ,
            IAttachmentService attachmentService)
        {
            _unitOfWork = unitOfWork;
            _attachmentService = attachmentService;
            //_employeeRepository = employeeRepository;
        }
        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(string search)
        {
           var employee = await _unitOfWork.EmployeeRepository
                .GetIQueryable()
                .Where(E => !E.IsDeleted && (string.IsNullOrEmpty(search) || E.Name.ToLower().Contains(search.ToLower())))
                .Include(E => E.Department)
                .Select(employee => new EmployeeDto() 
                                  { 
                                    Id = employee.Id,
                                    Name = employee.Name,
                                    Email = employee.Email,
                                    EmployeeType =employee.EmployeeType.ToString(),
                                    Age = employee.Age,
                                    Salary = employee.Salary,
                                    IsActive = employee.IsActive,
                                    Address = employee.Address,
                                    Gender = employee.Gender.ToString(),
                                    Department = employee.Department.Name
                                    ,Image = employee.Image
                                  }).AsNoTracking().ToListAsync();
            return employee;

           
        }

        public async Task<EmployeeDetailsDto?> GetEmployeeByIdAsync(int id)
        {
            var employee =await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
            if (employee is not null) 
            {
                return new EmployeeDetailsDto()
                {
                    Id  = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    IsActive = employee.IsActive,
                    Salary = employee.Salary,
                    PhoneNumber = employee.PhoneNumber,
                    Email = employee.Email,
                    HiringDate = employee.HiringDate,
                    Gender = employee.Gender,
                    EmployeeType = employee.EmployeeType,
                    CreatedBy = 1,
                    CreatedOn = DateTime.UtcNow,
                    LastModifiedBy=1,
                    LastModifiedOn= DateTime.UtcNow,
                  Department = employee.Department.Name,
                  Image = employee.Image,
                  
 
                };
                
            }
            return null;
        }
        public async Task<int> CreateEmployeeAsync(CreatedEmployeeDto employeeDto)
        {
        //    employeeDto.ImageName = _attachmentService.Upload(employeeDto.Image, "images");
            var employee = new Employee()
            {
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                IsActive = employeeDto.IsActive,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId = employeeDto.DepartmentId,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
             //Image = employeeDto.ImageName,
                
               

            };

            if (employeeDto.Image is not null)
            {
            employee.Image = await _attachmentService.UploadAsync(employeeDto.Image, "images");


            }


            _unitOfWork.EmployeeRepository.Add(employee);
           return await _unitOfWork.CompleteAsync();
        }


        public async Task<int> UpdateEmployeeAsync(UpdatedEmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                IsActive = employeeDto.IsActive,
                Salary= employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                Gender= employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId = employeeDto.DepartmentId,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,

            };
          
            _unitOfWork.EmployeeRepository.Update(employee);
            return await _unitOfWork.CompleteAsync();
        }
        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employeeRepo = _unitOfWork.EmployeeRepository;
            var employee = await employeeRepo.GetByIdAsync(id);
            if(employee is { })
            {
                 employeeRepo.Delete(employee) ;
            }
            return await _unitOfWork.CompleteAsync()>0;
        }

     

    }
}
