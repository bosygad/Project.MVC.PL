using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        //private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(/*IEmployeeRepository employeeRepository*/ IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_employeeRepository = employeeRepository;
        }
        public IEnumerable<EmployeeDto> GetEmployees(string search)
        {
           var employee = _unitOfWork.EmployeeRepository
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
                                  }).AsNoTracking().ToList();
            return employee;

           
        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
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
                  Department = employee.Department.Name
 
                };
                
            }
            return null;
        }
        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
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

            };

             _unitOfWork.EmployeeRepository.Add(employee);
           return _unitOfWork.Complete();
        }


        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
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
            return _unitOfWork.Complete();
        }
        public bool DeleteEmployee(int id)
        {
            var employeeRepo = _unitOfWork.EmployeeRepository;
            var employee = employeeRepo.GetById(id);
            if(employee is { })
            {
                 employeeRepo.Delete(employee) ;
            }
            return _unitOfWork.Complete()>0;
        }

     

    }
}
