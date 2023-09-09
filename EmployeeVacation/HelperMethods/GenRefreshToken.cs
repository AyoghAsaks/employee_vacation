using System.Security.Cryptography;

namespace EmployeeVacation.HelperMethods
{
    public class GenRefreshToken
    {
        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
