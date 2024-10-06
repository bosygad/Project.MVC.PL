using System.ComponentModel.DataAnnotations;

namespace Project.MVC.PL.ViewModels.Departments
{
    public class DepartmentEditViewModel
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;

        public string? Description { get; set; }

        [Display(Name = "Creation Date")]
        public DateOnly? CreatedDate { get; set; }
    }
}
