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
        #region Services
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
        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        } 
        #endregion

        #region Details
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
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto department)
        {
            var Message = string.Empty;
            if (!ModelState.IsValid)
            {
                return View(department);
            }
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

        #endregion

        #region Update
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
            }
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, DepartmentEditViewModel departmentViewModel)
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

                message = _environment.IsDevelopment() ? ex.Message : "An Erorr Has Occured during Updating The Department";



            }
            ModelState.AddModelError(string.Empty, message);
            return View(departmentViewModel);
        }

        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int? id)
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

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var deleted = _departmentService.DeleteDepartment(id);
                if (deleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "An Erorr Has Occured during Deleting The Department";
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);

                message = _environment.IsDevelopment() ? ex.Message : "An Erorr Has Occured during Deleting The Department";
            }
            //ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index));

        } 
        #endregion
    }
}