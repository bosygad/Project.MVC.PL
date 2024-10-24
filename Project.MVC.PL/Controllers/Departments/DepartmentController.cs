using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Project.BLL.Models.Departments;
using Project.BLL.Services.Departments;
using Project.DAL.Entities.Departments;
using Project.MVC.PL.ViewModels.Departments;

namespace Project.MVC.PL.Controllers.Departments
{
    [Authorize]
    public class DepartmentController : Controller
    {
        #region Services
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(
            IDepartmentService departmentService,
            IMapper mapper,
            ILogger<DepartmentController> logger,
            IWebHostEnvironment environment)
        {
            _departmentService = departmentService;
            _mapper = mapper;
            _logger = logger;
            _environment = environment;
        } 
        #endregion

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            //ViewData["Message"] ="ViweData";
            //ViewBag.Message = "ViewBag";
          

            var departments = await _departmentService.GetAllDepartmentsAsync();
            return View(departments);
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
                var department = await _departmentService.GetDepartmentByIdAsync(id.Value);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
        {
            var Message = string.Empty;
            if (!ModelState.IsValid)
            {
                return View(departmentVM);
            }
            try
            {
                var CreateDepartment =  _mapper.Map<CreatedDepartmentDto>(departmentVM);
                //var CreateDepartment = new CreatedDepartmentDto()
                //{

                //    Code = departmentVM.Code,
                //    Name = departmentVM.Name,
                //    Description = departmentVM.Description,
                //    CreatedDate = departmentVM.CreatedDate,

                //};


                var Created = await _departmentService.CreateDepartmentAsync(CreateDepartment) > 0;
                if (!Created)
                {
                    
                    //return RedirectToAction(nameof(Index));
                
                    TempData["Message"] = "Department is Not Created"; ;
                    //Message = "Department is Not Created";
                    ModelState.AddModelError(string.Empty, Message);
                   return View(departmentVM);
                }
               
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                Message = _environment.IsDevelopment() ? ex.Message : "An Erorr Has Occured during Creating The Department";
            }
            //ModelState.AddModelError(string.Empty, Message);
            //return View(departmentVM);
            TempData["Message"] = "Department is Created"; ;
            return RedirectToAction(nameof(Index));
        } 

        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            else
            {
                var department = await _departmentService.GetDepartmentByIdAsync(id.Value);
                if (department is null)
                {
                    return NotFound();
                }
                else
                {
                    var departmentVM = _mapper.Map< DepartmentViewModel>(department);
                    return View(departmentVM);
                    //    new DepartmentViewModel()
                    //{
                    //    Code = department.Code,
                    //    Name = department.Name,
                    //    Description = department.Description,
                    //    CreatedDate = department.CreatedDate,
                        
                    //});
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, DepartmentViewModel departmentViewModel)
        {
            var message = string.Empty;
            if (!ModelState.IsValid)
            {
                return View(departmentViewModel);
            }
            try
            {
                var UpdateDepartment = _mapper.Map<DepartmentViewModel, UpdatedDepartmentDto>(departmentViewModel);
              /// or //  var UpdateDepartment = _mapper.Map<UpdatedDepartmentDto>(departmentViewModel);
            
                //var UpdateDepartment = new UpdatedDepartmentDto()
                //{
                //    Id = id,
                //    Code = departmentViewModel.Code,
                //    Name = departmentViewModel.Name,
                //    Description = departmentViewModel.Description,
                //    CreatedDate = departmentViewModel.CreatedDate,

                //};
                var Updated = await _departmentService.UpdateDepartmentAsync(UpdateDepartment) > 0;
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return BadRequest();

            }
            else
            {
                var department = await _departmentService.GetDepartmentByIdAsync(id.Value);
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
          [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;
            try
            {
                
                var deleted = await _departmentService.DeleteDepartmentAsync(id);
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