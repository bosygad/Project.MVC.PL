using Microsoft.AspNetCore.Mvc;
using Project.BLL.Models.Employees;
using Project.BLL.Services.Employees;
using Project.MVC.PL.ViewModels.Employees;

namespace Project.MVC.PL.Controllers.Employees
{
    public class EmployeeController : Controller
    {
        #region Srevices
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _environment;


        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger, IWebHostEnvironment environment)
        {
            _employeeService = employeeService;
            _logger = logger;
            _environment = environment;
        }
        #endregion

        #region Index

        [HttpGet]
        public IActionResult Index()
        {
            var employee = _employeeService.GetAllEmployees();
            return View(employee);
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
                var employee = _employeeService.GetEmployeeById(id.Value);
                if (employee is null)
                {
                    return NotFound();
                }
                else
                {
                    return View(employee);
                }
            }
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create() { return View(); }

        [HttpPost]
        public IActionResult Create(CreatedEmployeeDto employeeDto) 
        {
            if (!ModelState.IsValid)
            {
                return View(employeeDto);
            }
            var message = string.Empty;
            try
            {
                var Result = _employeeService.CreateEmployee(employeeDto);
                 if(Result > 0)
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    message = "Employee IS Not Created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(employeeDto);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                message = _environment.IsDevelopment()? ex.Message : "An Erorr Has Occured during Creating The Employee";
            }
            ModelState.AddModelError(string.Empty , message);
            return View(employeeDto);
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
                var employee = _employeeService.GetEmployeeById(id.Value);
                if(employee is null)
                {
                    return NotFound();
                }
                else
                {
                    return View(new EmployeeEditViewModel()
                    {
                        Name = employee.Name,
                        Address = employee.Address,
                        Email = employee.Email,
                        Age = employee.Age,
                        Salary = employee.Salary,
                        PhoneNumber =employee.PhoneNumber,
                        IsActive = employee.IsActive,
                        HiringDate = employee.HiringDate,
                        EmployeeType = employee.EmployeeType,
                        Gender = employee.Gender

                    });
                }
            }
        
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id , EmployeeEditViewModel EmployeeViewModel) 
        {
        var message = string.Empty;
            if (!ModelState.IsValid) {
                return View(EmployeeViewModel);
            }
            try
            {
                var employee = new UpdatedEmployeeDto()
                {
                    Id = id,
                    Name = EmployeeViewModel.Name,
                    Address = EmployeeViewModel.Address,
                    Email  = EmployeeViewModel.Email,
                    Age= EmployeeViewModel.Age,
                    Salary = EmployeeViewModel.Salary,
                    PhoneNumber = EmployeeViewModel.PhoneNumber,
                    IsActive = EmployeeViewModel.IsActive,
                    HiringDate = EmployeeViewModel.HiringDate,
                    EmployeeType = EmployeeViewModel.EmployeeType,
                    Gender = EmployeeViewModel.Gender

                };
              var UpdatedEmployee = _employeeService.UpdateEmployee(employee) > 0;
                if (UpdatedEmployee)
                {
                    return RedirectToAction(nameof(Index));
                }
                else 
                {
                    message = "An Erorr Has Occured during Updating The Employee ";
                }
            }
            catch (Exception ex)
            {

               _logger.LogError(ex , ex.Message);
                message = _environment.IsDevelopment ()? ex.Message : "An Erorr Has Occured during Updating The Employee ";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(EmployeeViewModel);
        }
        #endregion

        #region Delete
      

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var deletedEmlpoyee = _employeeService.DeleteEmployee(id);
                if (deletedEmlpoyee)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {

                    message = "An Erorr Has Occured during Deleting The Employee";
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);

                message = _environment.IsDevelopment() ? ex.Message : "An Erorr Has Occured during Deleting The Employee";
            }
            //ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index));

        }

        #endregion
    }
}
