using Microsoft.AspNetCore.Identity;

namespace FamiliesAPI.Services.Security
{
    public class ValidatePass
    {
        public static bool ValidatePassword(string password, string hashedPassword, string hashKey)
        {
            var passwordHasher = new PasswordHasher<object>();
            var result = passwordHasher.VerifyHashedPassword(hashKey, hashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
