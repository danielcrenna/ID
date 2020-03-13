using System;
using ID.Configuration;
using ID.Internal;
using Microsoft.Extensions.Options;

namespace ID
{
    public sealed class SecureHashProvider : ISecureHashProvider
    {
        private readonly IOptionsMonitor<SecurityOptions> _options;

        public SecureHashProvider(IOptionsMonitor<SecurityOptions> options)
        {
            _options = options;
        }

        public ReadOnlySpan<byte> Password(ReadOnlySpan<byte> password)
        {
            const int size = 128;
            const long opsLimit = 3;

            Span<byte> buffer = new byte[size];
            
            var memLimit = (int) _options.CurrentValue.PasswordHashMaxBytes;
            
            unsafe
            {
                fixed (byte* b = buffer)
                {
                    fixed (byte* p = password)
                    {
                        Libsodium.crypto_pwhash_str(b, p, password.Length, opsLimit, memLimit);
                    }
                }
            }

            return buffer;
        }

        public VerifyHashResult PasswordVerify(ReadOnlySpan<byte> password, ReadOnlySpan<byte> hash)
        {
            unsafe
            {
                fixed (byte* p = &password.GetPinnableReference())
                {
                    fixed (byte* h = &hash.GetPinnableReference())
                    {
                        var result = Libsodium.crypto_pwhash_str_verify(h, p, password.Length);

                        if (result != 0)
                            return VerifyHashResult.Invalid;

                        return VerifyHashResult.Valid;
                    }
                }
            }
        }
    }
}