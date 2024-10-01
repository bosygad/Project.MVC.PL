using Microsoft.AspNetCore.Mvc;
using Project.BLL.Services.Departments;

namespace Project.MVC.PL.Controllers.Departments
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }
    }
}
