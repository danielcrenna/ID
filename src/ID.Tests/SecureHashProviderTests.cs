using System;
using System.Text;
using ID.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ID.Tests
{
    public class SecureHashProviderTests
    {
        public SecureHashProviderTests(IServiceCollection services)
        {
            services.Configure<SecurityOptions>(o => { });
            services.AddSingleton<ISecureHashProvider, SecureHashProvider>();
        }

        public bool Same_input_produces_same_hash(ISecureHashProvider provider)
        {
            const string password = "rosebud";

            var left = provider.Password(Encoding.UTF8.GetBytes(password));
            var right = provider.Password(Encoding.UTF8.GetBytes(password));
            return left.SequenceEqual(right);
        }

        public bool Different_input_produces_different_hash(ISecureHashProvider provider)
        {
            const string password = "rosebud";

            var left = provider.Password(Encoding.UTF8.GetBytes(password));
            var right = provider.Password(Encoding.UTF8.GetBytes("BoseBud"));
            return !left.SequenceEqual(right);
        }

        public bool Hash_can_be_verified(ISecureHashProvider provider)
        {
            const string password = "rosebud";
            
            var hash = provider.Password(Encoding.UTF8.GetBytes(password));
            var result = provider.PasswordVerify(Encoding.UTF8.GetBytes(password), hash);
            return result == VerifyHashResult.Valid;
        }
    }
}

