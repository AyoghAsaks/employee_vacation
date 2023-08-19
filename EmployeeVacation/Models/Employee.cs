using Microsoft.AspNetCore.Identity;

namespace EmployeeVacation.Models
{
    //The class "User" can also be named "Employee"
    public class Employee : IdentityUser
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set;} 
        public string? TaxId { get; set;}
        public DateTime DateOfBirth { get; set; } 
        public DateTime DateJoined { get; set;} = DateTime.Now;

    }
}
