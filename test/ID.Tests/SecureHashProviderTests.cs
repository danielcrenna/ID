using System;
using System.Text;
using ID.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ID.Tests
{
    public class SecureHashProviderTests
    {
        private const string Password = "rosebud";

        public SecureHashProviderTests(IServiceCollection services)
        {
            services.AddOptions();
            services.AddSingleton<ISecureHashProvider, SecureHashProvider>();
            services.Configure<SecurityOptions>(o => { });
        }

        public bool Same_input_produces_different_hash(ISecureHashProvider provider)
        {
            var bytes = Encoding.UTF8.GetBytes(Password);
            var left = provider.Password(bytes);    // ARGON2 w/ built-in salt
            var right = provider.Password(bytes);
            return !left.SequenceEqual(right);
        }

        public bool Different_input_produces_different_hash(ISecureHashProvider provider)
        {
            var leftBytes = Encoding.UTF8.GetBytes(Password);
            var rightBytes = Encoding.UTF8.GetBytes("rosebowl");

            var left = provider.Password(leftBytes);
            var right = provider.Password(rightBytes);
            return !left.SequenceEqual(right);
        }

        public bool Hash_can_be_verified(ISecureHashProvider provider)
        {
            var hash = provider.Password(Encoding.UTF8.GetBytes(Password));
            var result = provider.PasswordVerify(Encoding.UTF8.GetBytes(Password), hash);
            return result == VerifyHashResult.Valid;
        }

        public bool Invalid_hash_cannot_be_verified(ISecureHashProvider provider)
        {
            var hash = provider.Password(Encoding.UTF8.GetBytes(Password));
            var result = provider.PasswordVerify(Encoding.UTF8.GetBytes("rosebowl"), hash);
            return result == VerifyHashResult.Invalid;
        }
    }
}

