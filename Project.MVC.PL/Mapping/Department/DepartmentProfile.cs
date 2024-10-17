using AutoMapper;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Project.BLL.Models.Departments;
using Project.BLL.Models.Employees;
using Project.MVC.PL.ViewModels.Departments;
using Project.MVC.PL.ViewModels.Employees;

namespace Project.MVC.PL.Mapping.Department
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            
    
            CreateMap<DepartmentDetailsDto, DepartmentViewModel>();
            CreateMap<DepartmentViewModel, UpdatedDepartmentDto>();
            CreateMap<DepartmentViewModel, CreatedDepartmentDto>();


           
        }



    }
}
