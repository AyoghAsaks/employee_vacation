using EmployeeVacation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
/*
namespace EmployeeVacation.Configurations.Entities
{
    public class UserSeedConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Employee> builder)
        {
            var hasher = new PasswordHasher<Employee>();
            builder.HasData(
                new Employee
                {
                    Id ="4thyii-78hgy-uitdj",
                    Email = "admin@local.com",
                    NormalizedEmail = "ADMIN@LOCAL.COM",
                    Firstname = "System",
                    Lastname = "Admin",
                    PasswordHash = hasher.HashPassword(null, "Pa$$w0rd")
                },
                new Employee
                {
                    Id = "5jyyii-61uvc-yuber",
                    Email = "emp@local.com",
                    NormalizedEmail = "EMP@LOCAL.COM",
                    Firstname = "Ujay",
                    Lastname = "User",
                    PasswordHash = hasher.HashPassword(null, "Pa$$w0rd")
                }
           );
        }
    }
}
*/