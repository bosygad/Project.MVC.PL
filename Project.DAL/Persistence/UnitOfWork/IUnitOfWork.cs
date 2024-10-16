﻿using Project.DAL.Persistence.Repositories.Departments;
using Project.DAL.Persistence.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IEmployeeRepository EmployeeRepository { get;  }
        public IDepartmentRepository DepartmentRepository { get; }

        int Complete();
    }
}
