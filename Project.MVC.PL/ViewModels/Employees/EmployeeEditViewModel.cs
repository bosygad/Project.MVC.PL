using Project.DAL.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Project.MVC.PL.ViewModels.Employees
{
    public class EmployeeEditViewModel
    {
       
        public string Name { get; set; } = null!;

       
        public int? Age { get; set; }
      
        public string? Address { get; set; }

        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

       
        public string? Email { get; set; }

        [Display(Name = "Phone Number")]
       
        public string? PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateOnly? HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }

    }
}
