using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.DAL.Entities;
using Project.MVC.PL.ViewModels.Identity;

namespace Project.MVC.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #region SignUp
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
        {
            if (!ModelState.IsValid) {
                return BadRequest();
                
            }
             var User = await _userManager.FindByNameAsync(viewModel.UserName);


            if (User is { })
            {
                ModelState.AddModelError(nameof(SignUpViewModel.UserName), "The UserName is Already in Use Anthor Account ");
                return View(viewModel);
            }
                 User = new ApplicationUser
                {
                    FName = viewModel.FirstName,
                    LName = viewModel.LastName,
                    UserName = viewModel.UserName,
                    Email = viewModel.Email,
                    
                    
                    IsAgree = viewModel.IsAgree,

                };
                var Result = await _userManager.CreateAsync(User, viewModel.password);

                if (Result.Succeeded)
                {
                    return RedirectToAction(nameof(SignIn));
                }
                else
                {
                    foreach (var error in Result.Errors)
                    {
                    ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            return View(viewModel);
            

        }


        #endregion

        #region Sign In

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel viewModel)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var User = await _userManager.FindByEmailAsync(viewModel.Email);

            if (User is { })
            {
                var flag = await _userManager.CheckPasswordAsync(User, viewModel.password);
                if (flag)
                {
                    var result = await _signInManager.PasswordSignInAsync(User, viewModel.password, viewModel.RememberMe, true);
                    if (result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "Your Account is Not Confirm Yet !!");


                    if (result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, "Your Account Is Locked");


                    if (result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), "Home");




                }

                
            }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                return View(viewModel) ;
        }
        #endregion


         #region SignOut

       public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));


        }
        #endregion

       
    }
}
