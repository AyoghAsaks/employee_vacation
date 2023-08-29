using EmployeeVacation.Data;
using EmployeeVacation.Models;
using EmployeeVacation.Models.DTO;
using EmployeeVacation.Models.InputModels;
//using EmployeeVacation.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using User.Management.Service.Models;
using User.Management.Service.Services;
/*
namespace EmployeeVacation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        public AuthorizationController(ApplicationDbContext context, UserManager<Employee> userManager,
                                       RoleManager<IdentityRole> roleManager,
                                       ITokenService tokenService,
                                       IEmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        //Login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUser loginEmployee)
        {
            //checking the employee ...
            var employee = await _userManager.FindByNameAsync(loginEmployee.UserName);

            //-----------------------------------------checking the password 
            if (employee != null && await _userManager.CheckPasswordAsync(employee, loginEmployee.Password))
            {
                //claimlist creation
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, employee.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                //we add roles to the claimlist
                var employeeRoles = await _userManager.GetRolesAsync(employee);
                foreach (var employeeRole in employeeRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, employeeRole));
                }

                //generate the token with the claims...
                var token = _tokenService.GetToken(authClaims);

                //generate the refresh token
                var refreshToken = _tokenService.GetRefreshToken();
                
                //Find and retrieve the token from the database table "TokenInfos"
                var tokenInfo = _context.TokenInfos.FirstOrDefault(a => a.UserName == employee.UserName);
                if (tokenInfo == null)
                {
                    //Assign the class "TokenInfo" with data from the database table "TokenInfos" 
                    var info = new TokenInfo
                    {
                        UserName = employee.UserName,
                        RefreshToken = refreshToken,
                        RefreshTokenExpiry = DateTime.Now.AddDays(7)

                    };
                    _context.TokenInfos.Add(info); //Add info to database
                }
                else
                {
                    tokenInfo.RefreshToken = refreshToken;
                    tokenInfo.RefreshTokenExpiry = DateTime.Now.AddDays(1);
                }
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                //returning the token
                return Ok(new LoginResponse
                {
                    UserName= employee.UserName,
                    Token = token.TokenString,
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo,
                    StatusCode = 1,
                    Message = "Logged In"
                });
            }
            //login failed condition
            return Ok(new LoginResponse 
            { 
                StatusCode = 0,
                Message = "Invalid Username or Password",
                Token = "", 
                Expiration = null
            });
        }

        //Registration
        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Registration([FromBody] RegisterUser registerEmployee)
        {
            var status = new Status();
            //Check Validations
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Please enter all the required fields";
                return Ok(status);
            }
            
            //Check if this Employee Exist
            var employeeExist = await _userManager.FindByEmailAsync(registerEmployee.Email);
            if (employeeExist == null)
            {
                status.StatusCode = 0;
                status.Message = "Please enter all the required fields";
                return Ok(status);
            }

            //Assign values to the Employee object.
            Employee employee = new()
            {
                Firstname = registerEmployee.Firstname,
                Lastname = registerEmployee.Lastname,
                DateOfBirth = registerEmployee.DateOfBirth,
                DateJoined = registerEmployee.DateJoined,
                Email = registerEmployee.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerEmployee.UserName
            };

            //Creates an Employee.
            var result = await _userManager.CreateAsync(employee, registerEmployee.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "Employee creation failed";
                return Ok(status);
            }
            
            //Add roles here
            if(!await _roleManager.RoleExistsAsync(UserRoles.Employee))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Employee));

            if (await _roleManager.RoleExistsAsync(UserRoles.Employee))
            {
                await _userManager.AddToRoleAsync(employee, UserRoles.Employee);
            }
            status.StatusCode = 1;
            status.Message = "Successfully Registered";
            return Ok(status);

            /*
            //Add roles to the employee. The roles here are "Admin" & "Employee"
            if (!await _roleManager.RoleExistsAsync(UserRoles.Employee))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Employee));   

            if (await _roleManager.RoleExistsAsync(UserRoles.Employee))
            {
                await _userManager.AddToRoleAsync(employee, UserRoles.Employee);
            }
            */
            /**********************************************************
            if (await _roleManager.RoleExistsAsync(role))
            {
                var result = await _userManager.CreateAsync(employee, registerEmployee.Password);
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
                var message = new Message(new string[] { employee.Email! }, "Confirmation email link", confirmationLink!);
                _emailService.SendEmail(message);

                return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Message = $"The Employee has been created and the email link has been sent to the email: {employee.Email}" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response { Status = "Error", Message = "This Role Does not Exist." });
            }
            ***************************************************************/
        /*}*/
       
        /*
        //Confirm email
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var employee = await _userManager.FindByEmailAsync(email);
            if (employee != null)
            {
                var result = await _userManager.ConfirmEmailAsync(employee, token);
                if (result.Succeeded)
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
*/