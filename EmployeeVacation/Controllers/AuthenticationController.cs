using EmployeeVacation.Models;
using EmployeeVacation.Models.InputModels;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User.Management.Service.Models; ////
using User.Management.Service.Services; ////

//using NETCore.MailKit.Core;
//using System.Security.Cryptography.X509Certificates;


namespace EmployeeVacation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService; 
        public AuthenticationController(UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager, IEmailService emailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
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

        //Login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUser loginUser)
        {
            //checking the employee ...
            var employee = await _userManager.FindByNameAsync(loginUser.UserName);

            //-----------------------------------------checking the password 
            if (employee != null && await _userManager.CheckPasswordAsync(employee, loginUser.Password))
            {
                //claimlist creation
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, employee.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                //we add roles to the claimlist
                var userRoles = await _userManager.GetRolesAsync(employee);
                foreach(var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                //generate the token with the claims...
                var jwtToken = GetToken(authClaims);

                //returning the token...
                return Ok(new
                { 
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expiration = jwtToken.ValidTo
                });
            }

            return Unauthorized();
        }

        //Method "JwtSecurityToken" that generates the token 
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
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
