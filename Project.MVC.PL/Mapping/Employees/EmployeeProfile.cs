using AutoMapper;
using Project.BLL.Models.Employees;
using Project.MVC.PL.ViewModels.Employees;

namespace Project.MVC.PL.Mapping.Employees
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        { 
        CreateMap<EmployeeViewModel, UpdatedEmployeeDto>();
            CreateMap<EmployeeDetailsDto, EmployeeViewModel>();

        }
    }
}
