using System.Text;
using Microsoft.AspNetCore.Identity;

namespace ID.AspNetCore.Identity
{
    public class PasswordHasher<TUser> : IPasswordHasher<TUser> where TUser : class
    {
        private readonly ISecureHashProvider _secureHashProvider;

        public PasswordHasher(ISecureHashProvider secureHashProvider)
        {
            _secureHashProvider = secureHashProvider;
        }

        public string HashPassword(TUser user, string password)
        {
            throw new System.NotImplementedException();
        }

        public PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
        {
            throw new System.NotImplementedException();
        }
    }
}
