﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Models
{
    public class Department : ModelBase
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;

        public string? Description { get; set; }
        public DateOnly? CreatedDate { get; set; }
    }
}
