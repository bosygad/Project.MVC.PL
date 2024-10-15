using AutoMapper;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Project.BLL.Models.Departments;
using Project.MVC.PL.ViewModels.Departments;

namespace Project.MVC.PL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Employee

            #endregion
            #region Department
            CreateMap<DepartmentDetailsDto, DepartmentViewModel>();
            CreateMap<DepartmentViewModel, UpdatedDepartmentDto>();
            CreateMap<DepartmentViewModel, CreatedDepartmentDto>();
            

        #endregion

        }



    }
}
