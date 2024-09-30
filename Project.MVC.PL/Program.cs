using Microsoft.EntityFrameworkCore;
using Project.BLL.Services.Departments;
using Project.DAL.Persistence.Data.Contexts;
using Project.DAL.Persistence.Repositories.Departments;

namespace Project.MVC.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            builder.Services.AddDbContext<ApplicationDbContext>((optionsBuilder) => {
                optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDepartmentRepository , DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService , DepartmentService>();
            //builder.Services.AddScoped<DbContextOptions<ApplicationDbContext>>((ServiceProvider =>
            //{
            //    //var options = new DbContextOptions<ApplicationDbContext>();
            //    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //    optionsBuilder.UseSqlServer("");
            //  var options = optionsBuilder.Options;
            //    return options;
            //}));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
