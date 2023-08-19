using EmployeeVacation.Models;
using Microsoft.AspNetCore.Identity;
/*
namespace EmployeeVacation.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<Employee> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new Employee
                {
                   //UserName = "bob",
                   //Email = "bob@test.com"
                   Id = "408aa-3d84-hy7889",
                   Email = "bob@test.com",
                   Firstname = "Bob",
                   Lastname = "User"
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "Employee");

                var admin = new User
                {
                    //UserName = "admin",
                    //Email = "admin@test.com"
                    Id = "409ab-3a09-yu9089",
                    Email = "admin@test.com",
                    Firstname = "Adama",
                    Lastname = "Admin"
                };
                await userManager.CreateAsync(admin, "Pa$$w0rd");
                await userManager.AddToRolesAsync(admin, new[] { "Employee", "Admin" });
            }
        }
        
    }
}
*/