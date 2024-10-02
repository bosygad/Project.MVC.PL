using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.DAL.Common.Enums;
using Project.DAL.Entities.Empeloyees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Persistence.Data.Configuration.Employees
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
           builder.Property(E => E.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(E => E.Address).HasColumnType("varchar(200)");
            builder.Property(E => E.Salary).HasColumnType("decimal(8,2)");

            builder.Property(E => E.Gender).HasConversion((gender)=>gender.ToString() , (gender)=> (Gender)Enum.Parse(typeof(Gender),gender) );
            builder.Property(E => E.EmployeeType).HasConversion((EmployeeType)=>EmployeeType.ToString() , (EmployeeType)=> (EmployeeType)Enum.Parse(typeof(EmployeeType),EmployeeType) );
            builder.Property(E => E.CreatedOn).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
