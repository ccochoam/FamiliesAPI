using Microsoft.AspNetCore.Identity;

namespace FamiliesAPI.Services.Security
{
    public class PasswordHasher
    {
        public static string EncryptKey(string key, string salt)
        {
            string hashedPassword = HashPassword(key, salt);
            return hashedPassword;
        }

        private static string HashPassword(string password, string salt)
        {
            var passwordHasher = new PasswordHasher<object>();
            return passwordHasher.HashPassword(salt, password);
        }
    }
}
