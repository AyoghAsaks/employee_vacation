using System.ComponentModel.DataAnnotations;

namespace EmployeeVacation.Models.InputModels
{
    public class LoginUser
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? UserName { get; set; } 

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
