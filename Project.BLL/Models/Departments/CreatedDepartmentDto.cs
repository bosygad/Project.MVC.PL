using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Models.Departments
{
    public class CreatedDepartmentDto
    {
        public string Name { get; set; } = null!;
        [Required(ErrorMessage ="Code Is Required")]
        public string Code { get; set; } = null!;
        public string? Description { get; set; }

        [Display(Name = "Date Of Creation")]
        public DateOnly? CreatedDate { get; set; }

       
    }
}
