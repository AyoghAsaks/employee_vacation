using EmployeeVacation.Auth;
using EmployeeVacation.Models;
using EmployeeVacation.Models.InputModels;
using Microsoft.AspNetCore.Identity;
using EmployeeVacation.IRepositories;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Data;
using System;
using System.Net.NetworkInformation;

using User.Management.Service.Services; //// for IEmailService

namespace EmployeeVacation.Repositories
{
    public class UserManagement : IUserManagement
    {
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public UserManagement
        (
            UserManager<Employee> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IConfiguration configuration, 
            IEmailService emailService
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<ApiResponse<List<string>>> AssignRoleToUserAsync(List<string> roles, Employee employee)
        {
            var assignedRole = new List<string>();
            foreach (var role in roles)
            {
                if(await _roleManager.RoleExistsAsync(role))
                {
                    if(!await _userManager.IsInRoleAsync(employee, role))
                    {
                        await _userManager.AddToRoleAsync(employee, role);
                        assignedRole.Add(role);
                    }
                }
            }

            return new ApiResponse<List<string>> { IsSuccess = true, StatusCode = 200, Message="Role has been assigned",
            Response=assignedRole};
        }

        public async Task<ApiResponse<CreateEmployeeResponse>> CreateUserWithTokenAsync(RegisterUser registerUser)
        {
            //Check if this Employee Exist
            var userExist = await _userManager.FindByEmailAsync(registerUser.Email);
            if (userExist != null)
            {
                return new ApiResponse<CreateEmployeeResponse> { IsSuccess = false, StatusCode = 403, Message = "Employee already exists!" };
                /*return StatusCode(StatusCodes.Status403Forbidden,
                    new Response { Status = "Error", Message = "Employee already exists!" });
                */
            }

            //Add the Employee to the database
            Employee employee = new()
            {
                Firstname = registerUser.Firstname,
                Lastname = registerUser.Lastname,
                DateOfBirth = registerUser.DateOfBirth,
                DateJoined = registerUser.DateJoined,
                Email = registerUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerUser.UserName
            };
            var result = await _userManager.CreateAsync(employee, registerUser.Password);
            if(result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(employee);
                return new ApiResponse<CreateEmployeeResponse> 
                            { 
                                Response = new CreateEmployeeResponse() 
                                               { 
                                                    GivenEmployee = employee, 
                                                    Token = token 
                                               },
                                               IsSuccess = true, 
                                               StatusCode = 201, 
                                               Message = "Employee created successfully" 
                             };
            }
            else
            {
                return new ApiResponse<CreateEmployeeResponse> { IsSuccess = false, StatusCode = 500, Message = "Employee failed to be created" };
            }

        }

        
    }
}
