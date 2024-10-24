using System.ComponentModel.DataAnnotations;

namespace Project.MVC.PL.ViewModels.Identity
{
    public class SignInViewModel
    {
       

        [EmailAddress]
        public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
        public string password { get; set; } = null!;

        public bool RememberMe { get; set; }    
    }
}
