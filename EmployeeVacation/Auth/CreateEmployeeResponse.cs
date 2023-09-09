using EmployeeVacation.Models;

namespace EmployeeVacation.Auth
{
    public class CreateEmployeeResponse
    {
        public string Token { get; set; }

        public Employee GivenEmployee{ get; set; }
    }
}
