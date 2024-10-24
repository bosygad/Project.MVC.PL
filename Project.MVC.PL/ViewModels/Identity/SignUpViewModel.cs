using System.ComponentModel.DataAnnotations;

namespace Project.MVC.PL.ViewModels.Identity
{
    public class SignUpViewModel
    {
        [Display(Name ="First Name")]
        public string FirstName { get; set; } = null!;
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Username Is Requird")]
        public string UserName { get; set; } = null!;

        [EmailAddress]
        public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
        public string password { get; set; } = null!;
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Confirm Password Doesn't match With Password")]
        public string ConfirmPassword { get; set; } = null!;

        [Required]

        public bool IsAgree { get; set; }
    }
}
