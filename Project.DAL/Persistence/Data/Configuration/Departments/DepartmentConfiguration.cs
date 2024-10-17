using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.DAL.Entities.Departments;
using Project.DAL.Entities.Empeloyees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Persistence.Data.Configuration.Departments
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            //Fluent Apis For "Deepartment" Entites
            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            builder.Property(D => D.Code).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(D => D.Name).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            builder.Property(D => D.CreatedOn).HasDefaultValueSql("GETUTCDATE()");
           builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("Convert(date,GETDATE())");
            builder.HasMany(D => D.Employees)
                   .WithOne(E => E.Department)
                   .HasForeignKey(E => E.DepartmentId)
                   .OnDelete(DeleteBehavior.SetNull);


        }
    }
}
