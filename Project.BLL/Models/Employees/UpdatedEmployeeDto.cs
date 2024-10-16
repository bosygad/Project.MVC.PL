using Project.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Models.Employees
{
    public class UpdatedEmployeeDto
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "Max Lenght Of Name Is 50 Char")]
        [MinLength(5, ErrorMessage = "Min Lenght Of Name Is 5 Char")]
        public string Name { get; set; } = null!;

        [Range(21, 70)]
        public int? Age { get; set; }
        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$"
                           , ErrorMessage = "Address Must Be Like 123-Street-city-Country")]
        public string? Address { get; set; }

        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateOnly? HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public int? DepartmentId { get; set; }

        public string? Image { get; set; }

    }
}
