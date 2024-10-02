using Project.DAL.Common.Enums;
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
       
        public string Name { get; set; }
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
    }
}
