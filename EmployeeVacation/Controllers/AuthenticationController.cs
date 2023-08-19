using EmployeeVacation.Models;
using EmployeeVacation.Models.InputModels;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using NETCore.MailKit.Core;
using User.Management.Service.Models;

//using System.Security.Cryptography.X509Certificates;
////using User.Management.Service.Models; ////
using User.Management.Service.Services; ////

namespace EmployeeVacation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService; ////


        //public AuthenticationController(UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        public AuthenticationController(UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            //_configuration = configuration;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser, string role)
        {
            //Check if this Employee Exist
            var userExist = await _userManager.FindByEmailAsync(registerUser.Email);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    new Response { Status = "Error", Message = "Employee already exists!" });
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
            if(await _roleManager.RoleExistsAsync(role))
            {
                var result = await _userManager.CreateAsync(employee, registerUser.Password);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response { Status = "Error", Message = "The employee failed to be created" });
                }

                //Add role to the user/employee.... ////
                await _userManager.AddToRoleAsync(employee, role);

                //Add Token to Verify the email.... ////
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(employee);
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = employee.Email }, Request.Scheme);
                var message = new Message(new string[] {employee.Email! }, "Confirmation email link", confirmationLink!);
                _emailService.SendEmail(message);

                return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Message = $"The Employee has been created and the email link has been sent to the email: {employee.Email}" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response { Status = "Error", Message = "This Role Does not Exist." });
            }

        }

        /*
        [HttpGet]
        public IActionResult TestEmail()
        {
            var message = new Message(new string[] { "aayogh@yahoo.com" }, "Test", "<h1>Hello World!</h1>");

            _emailService.SendEmail(message);

            return StatusCode(StatusCodes.Status200OK,
                new Response { Status = "Success", Message = "Email Sent Successfully" });
        }
        */
        
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var employee = await _userManager.FindByEmailAsync(email);
            if (employee != null)
            {
                var result = await _userManager.ConfirmEmailAsync(employee, token);
                if(result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Message = "Email Verified Successfully" });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response { Status = "Error", Message = "This employee does not exist!" });
        }
        

    }
}
