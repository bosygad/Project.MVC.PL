using AutoMapper;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.Common.Services.Attachments;
using Project.BLL.Models.Employees;
using Project.BLL.Services.Departments;
using Project.BLL.Services.Employees;
using Project.MVC.PL.ViewModels.Employees;
using System.Reflection.Metadata;

namespace Project.MVC.PL.Controllers.Employees
{
    public class EmployeeController : Controller
    {
        #region Srevices
        private readonly IEmployeeService _employeeService;
      
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly IAttachmentService _attachmentService;

        public EmployeeController(IEmployeeService employeeService,
           
            ILogger<EmployeeController> logger,
            IMapper mapper,
            IWebHostEnvironment environment,
            IAttachmentService attachmentService)
        {
            _employeeService = employeeService;
           
            _logger = logger;
            _mapper = mapper;
            _environment = environment;
            _attachmentService = attachmentService;
        }
        #endregion

        #region Index

        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {

            var employee = await _employeeService.GetEmployeesAsync(search);

          

            return View(employee);
        }

       
        #endregion

        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            else
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);
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
        public IActionResult Create(/*[FromServices]IDepartmentService departmentService*/) {
            //ViewData["Departments"] = departmentService.GetAllDepartments();
            return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM) 
        {
            if (!ModelState.IsValid)
            {
                return View(employeeVM);
            }
            var message = string.Empty;
            try
            {
            /// employeeVM.ImageName = _attachmentService.Upload(employeeVM.Image, "images");
                //  var employee = _mapper.Map<CreatedEmployeeDto>(employeeVM);
                var employee = new CreatedEmployeeDto()
                {

                    Name = employeeVM.Name,
                    Address = employeeVM.Address,
                    Email = employeeVM.Email,
                    Age = employeeVM.Age,
                    Salary = employeeVM.Salary,
                    PhoneNumber = employeeVM.PhoneNumber,
                    IsActive = employeeVM.IsActive,
                    HiringDate = employeeVM.HiringDate,
                    EmployeeType = employeeVM.EmployeeType,
                    Gender = employeeVM.Gender,
                    DepartmentId = employeeVM.DepartmentId,
                    Image = employeeVM.Image,



                };
                var Result = await _employeeService.CreateEmployeeAsync(employee);
                 if(Result > 0)
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    message = "Employee IS Not Created";
                    //ModelState.AddModelError(string.Empty, message);
                    //return View(employeeDto);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                message = _environment.IsDevelopment()? ex.Message : "An Erorr Has Occured during Creating The Employee";
            }
            ModelState.AddModelError(string.Empty , message);
            return View(employeeVM);
        }

        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Edit(int? id /*, [FromServices] IDepartmentService departmentService*/) 
        {
            if (id is null)
            {
                return BadRequest();
            }
            else
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);
                if(employee is null)
                {
                    return NotFound();
                }
                else
                {
                    //ViewData["Departments"] = departmentService.GetAllDepartments();
                //    var UpdatedEmployee = _mapper.Map<EmployeeDetailsDto,EmployeeViewModel>(employee);
                    
                    //return View(UpdatedEmployee);
                    return View(new EmployeeViewModel()
                    {
                        Name = employee.Name,
                        Address = employee.Address,
                        Email = employee.Email,
                        Age = employee.Age,
                        Salary = employee.Salary,
                        PhoneNumber = employee.PhoneNumber,
                        IsActive = employee.IsActive,
                        HiringDate = employee.HiringDate,
                        EmployeeType = employee.EmployeeType,
                        Gender = employee.Gender,
                        DepartmentId = employee.DepartmentId,
                       // Image = employee.Image,





                    });
                }
            }
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id , EmployeeViewModel EmployeeViewModel) 
        {
        var message = string.Empty;
            if (!ModelState.IsValid) {
                return View(EmployeeViewModel);
            }
            try
            {
                // var employee = _mapper.Map<UpdatedEmployeeDto>(EmployeeViewModel);
                var employee = new UpdatedEmployeeDto()
                {
                    Id = id,
                    Name = EmployeeViewModel.Name,
                    Address = EmployeeViewModel.Address,
                    Email = EmployeeViewModel.Email,
                    Age = EmployeeViewModel.Age,
                    Salary = EmployeeViewModel.Salary,
                    PhoneNumber = EmployeeViewModel.PhoneNumber,
                    IsActive = EmployeeViewModel.IsActive,
                    HiringDate = EmployeeViewModel.HiringDate,
                    EmployeeType = EmployeeViewModel.EmployeeType,
                    Gender = EmployeeViewModel.Gender,
                    DepartmentId = EmployeeViewModel.DepartmentId,
                  //  Image = EmployeeViewModel.Image,

                };
                var UpdatedEmployee = await _employeeService.UpdateEmployeeAsync(employee) > 0;
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
           // ModelState.AddModelError(string.Empty, message);
           // return View(EmployeeViewModel);
           return RedirectToAction(nameof(Index)); 
        }
        #endregion

        #region Delete
      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var deletedEmlpoyee = await _employeeService.DeleteEmployeeAsync(id);
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
