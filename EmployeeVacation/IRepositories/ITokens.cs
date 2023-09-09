using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EmployeeVacation.IRepositories
{
    public interface ITokens
    {
        JwtSecurityToken CreateToken(List<Claim> authClaims);

        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
    }
}
