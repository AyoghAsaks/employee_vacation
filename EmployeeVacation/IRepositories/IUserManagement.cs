using EmployeeVacation.Auth;
using EmployeeVacation.Models;
using EmployeeVacation.Models.InputModels;

namespace EmployeeVacation.IRepositories
{
    public interface IUserManagement
    {
        ///<summary>
        ///Description of what the method does.
        ///</summary>
        ///<param name="RegisterUser">Description of the parameter</param>
        ///<returns>Description of the return value</returns>
        Task<ApiResponse<CreateEmployeeResponse>> CreateUserWithTokenAsync(RegisterUser registerUser);

        Task<ApiResponse<List<string>>> AssignRoleToUserAsync(List<string> roles, Employee employee);

    }
}
