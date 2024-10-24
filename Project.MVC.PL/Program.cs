using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.BLL.Common.Services.Attachments;
using Project.BLL.Services.Departments;
using Project.BLL.Services.Employees;
using Project.DAL.Entities;
using Project.DAL.Persistence.Data.Contexts;
using Project.DAL.Persistence.Repositories.Departments;
using Project.DAL.Persistence.Repositories.Employees;
using Project.DAL.Persistence.UnitOfWork;
using Project.MVC.PL.Mapping;
using Project.MVC.PL.Mapping.Department;
using Project.MVC.PL.Mapping.Employees;

namespace Project.MVC.PL
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
           


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            builder.Services.AddDbContext<ApplicationDbContext>(optionsBuilder => {
                optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            //builder.Services.AddScoped<IDepartmentRepository , DepartmentRepository>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IUnitOfWork , UnitOfWork>();
            builder.Services.AddScoped<IDepartmentService , DepartmentService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddTransient<IAttachmentService , AttachmentService>();

            builder.Services.AddAutoMapper(M => M.AddProfile(new DepartmentProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));



            builder.Services.AddIdentity<ApplicationUser , IdentityRole>((Options) =>
            {
                Options.Password.RequiredLength = 5;
                Options.Password.RequireNonAlphanumeric = true;
                Options.Password.RequireUppercase = true;
                Options.Password.RequireLowercase = true;
                Options.Password.RequiredUniqueChars = 1;


                Options.User.RequireUniqueEmail = true;
                //Options.User.AllowedUserNameCharacte
                Options.Lockout.AllowedForNewUsers = true;
                Options.Lockout.MaxFailedAccessAttempts = 5;
                Options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);

            })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/SignIn";
                options.AccessDeniedPath = "/Home/Error";
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                // options.LogoutPath = "/Account/SignIn";
                //   options.ForwardSignOut = "/Account/SignIn";
              

            });

         
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Identity.Application";
                options.DefaultChallengeScheme = "Identity.Application";

            }).AddCookie("Admin", ".AspNetCore.Admin", options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Home/Error";
                options.ExpireTimeSpan = TimeSpan.FromDays(10);
                options.LogoutPath = "/Account/SignIn";
            });
          
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

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
