using System;

namespace ID
{
    public interface ISecureHashProvider
    {
        ReadOnlySpan<byte> Password(ReadOnlySpan<byte> password);
        VerifyHashResult PasswordVerify(ReadOnlySpan<byte> password, ReadOnlySpan<byte> hash);
    }
}