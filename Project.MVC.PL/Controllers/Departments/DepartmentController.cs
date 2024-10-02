using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Project.BLL.Models.Departments;
using Project.BLL.Services.Departments;
using Project.DAL.Entities.Departments;
using Project.MVC.PL.ViewModels.Departments;

namespace Project.MVC.PL.Controllers.Departments
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(
            IDepartmentService departmentService,
            ILogger<DepartmentController> logger,
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
        public IActionResult Details(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            else
            {
                var department = _departmentService.GetDepartmentById(id.Value);
                if (department is null)
                {
                    return NotFound();
                }
                else
                {
                    return View(department);
                }
            }
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
                    ModelState.AddModelError(string.Empty, Message);
                    return View(department);
                }

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                Message = _environment.IsDevelopment() ? ex.Message : "An Erorr Has Occured during Creating The Department";
             }
            ModelState.AddModelError(string.Empty, Message);
            return View(department);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            else
            {
                var department = _departmentService.GetDepartmentById(id.Value);
                if (department is null)
                {
                    return NotFound();
                }
                else
                {
                    return View(new DepartmentEditViewModel()
                    {
                        Code = department.Code,
                        Name = department.Name,
                        Description = department.Description,
                        CreatedDate = department.CreatedDate,
                    });
                }
            } }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id ,DepartmentEditViewModel departmentViewModel)
        {
            var message = string.Empty;
            if (!ModelState.IsValid)
            {
                return View(departmentViewModel);
            }
            try
            {
                var UpdateDepartment = new UpdatedDepartmentDto()
                {
                    Id = id,
                    Code = departmentViewModel.Code,
                    Name = departmentViewModel.Name,
                    Description = departmentViewModel.Description,
                    CreatedDate = departmentViewModel.CreatedDate,
                };
                var Updated = _departmentService.UpdateDepartment(UpdateDepartment) > 0;
                if (Updated)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "An Erorr Has Occured during Updating The Department ";
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);

                message = _environment.IsDevelopment()?ex.Message : "An Erorr Has Occured during Updating The Department";

                
                
            }
            ModelState.AddModelError(string.Empty, message);
            return View(departmentViewModel);
        }


    }
}