using Microsoft.AspNetCore.Mvc;
using Project.BLL.Models.Departments;
using Project.BLL.Services.Departments;

namespace Project.MVC.PL.Controllers.Departments
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(
            IDepartmentService departmentService ,
            ILogger<DepartmentController> logger ,
            IWebHostEnvironment environment)
        {
            _departmentService = departmentService;
            _logger = logger;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto department)
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }
            var Message = string.Empty;
            try
            {
       
                    var Result = _departmentService.CreateDepartment(department);
                    if (Result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                    Message = "Department is Not Created";
                        ModelState.AddModelError(string.Empty,Message );
                       return View(department);
                    }
                
            }
            catch (Exception ex)
            {

                _logger.LogError(ex,ex.Message);

                if (_environment.IsDevelopment())
                {
                    Message = ex.Message;
                    return View(department);
                }

                else
                {
                    Message = "Department is Not Created";

                return View("Erorr " , Message);
                }

            }

        }
    }
}
