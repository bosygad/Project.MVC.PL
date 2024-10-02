using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Models.Departments
{
    public class DepartmentDto
    {

        public int Id { get; set; }
  
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;

        
        [Display(Name = "Date Of Creation")]
        public DateOnly? CreatedDate { get; set; }
    }
}
