using EmployeeVacation.Auth;
using EmployeeVacation.HelperMethods;
using EmployeeVacation.IRepositories;
using EmployeeVacation.Models;
using EmployeeVacation.Models.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using User.Management.Service.Models; ////
using User.Management.Service.Services; ////for IEmailService

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

        private readonly IUserManagement _userManagement;
        private readonly ITokens _token;
        public AuthenticationController(UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager, 
            IEmailService emailService, 
            IConfiguration configuration,
            IUserManagement userManagement,
            ITokens token
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _configuration = configuration;
            _userManagement = userManagement;
            _token = token;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
        {
            var tokenResponse = await _userManagement.CreateUserWithTokenAsync(registerUser); 
            if (tokenResponse.IsSuccess)
            {
                await _userManagement.AssignRoleToUserAsync(registerUser.Roles, tokenResponse.Response.GivenEmployee);
                //Add Token to Verify the email....
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { tokenResponse.Response.Token, email = registerUser.Email }, Request.Scheme);
                var message = new Message(new string[] { registerUser.Email! }, "Confirmation email link", confirmationLink!);
                _emailService.SendEmail(message);

                return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Message = $"The Employee has been created and the email link has been sent to the email: {registerUser.Email}", IsSuccess = true});

            }
            /*
            return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response { Status = "Failed", Message = "The Employee failed to be created" });
            */
            return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response { Message = tokenResponse.Message, IsSuccess=false });

            

        }
        /*
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
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                //we add roles to the claimlist
                var employeeRoles = await _userManager.GetRolesAsync(employee);
                foreach(var role in employeeRoles)
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
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                //we add roles to the claimlist
                var employeeRoles = await _userManager.GetRolesAsync(employee);
                foreach (var role in employeeRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                //generate the token with the claims...
                ////var token = CreateToken(authClaims); ////
                var token = _token.CreateToken(authClaims);

                ////var refreshToken = GenerateRefreshToken(); ////
                var refreshToken = GenRefreshToken.GenerateRefreshToken();

                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

                employee.RefreshToken = refreshToken;
                employee.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

                await _userManager.UpdateAsync(employee);

                return Ok(new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        //refresh token
        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;

            ////var principal = GetPrincipalFromExpiredToken(accessToken); ////
            var principal = _token.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            string username = principal.Identity.Name;

            var employee = await _userManager.FindByNameAsync(username);

            if (employee == null || employee.RefreshToken != refreshToken || employee.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid access token or refresh token");
            }
            ////var newAccessToken = CreateToken(principal.Claims.ToList()); ////
            var newAccessToken = _token.CreateToken(principal.Claims.ToList());

            ////var newRefreshToken = GenerateRefreshToken(); ////
            var newRefreshToken = GenRefreshToken.GenerateRefreshToken();

            employee.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(employee);

            return new ObjectResult(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken
            });
        }

        //Create Token <--------------------------------------------------------------
        /*
        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        */

        //GenerateRefreshToken  <-----------------------------------------
        /*
        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        */

        //GetPrincipal GetPrincipalFromExpiredToken <----------------------
        /*
        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid Token");
            }

            return principal;
        }
        */

        //revoke/logout a logged in employee and prevent them from logging in again.
        [Authorize]
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            var employee = await _userManager.FindByNameAsync(username);
            if (employee == null)
            {
                return BadRequest("Invalid user name");
            }

            employee.RefreshToken = null;
            await _userManager.UpdateAsync(employee);

            return NoContent();
        }

        //revoke/logout all logged in employees and prevent them from logging in again.
        [Authorize]
        [HttpPost]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            var employees = _userManager.Users.ToList();

            foreach (var employee in employees) 
            { 
                employee.RefreshToken = null;
                await _userManager.UpdateAsync(employee);
            }

            return NoContent();
        }

    }
}
