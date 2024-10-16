using Project.DAL.Common.Enums;
using Project.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Entities.Empeloyees
{
    public class Employee : ModelBase
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

        [Display(Name ="Hiring Date")]
        public DateOnly? HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }

        public int? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }

        public string? Image {  get; set; }
    }
}
