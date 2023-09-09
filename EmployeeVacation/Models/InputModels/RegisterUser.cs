using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeVacation.Models.InputModels
{
    public class RegisterUser
    {

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string? Firstname { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string? Lastname { get; set; } 

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; } 

        [Display(Name = "Date Joined")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date joined is required")]
        public DateTime DateJoined { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [NotMapped]
        public string ConfirmPassword { get; set; } = null!;

        public List<string>? Roles { get; set; }
    }
}
