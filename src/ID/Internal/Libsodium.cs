using System.Runtime.InteropServices;

namespace ID.Internal
{
    /// <summary>
    /// https://libsodium.gitbook.io/doc
    /// </summary>
    internal static class Libsodium
    {
        private const string DllName = "libsodium";

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void sodium_init();

        /// <summary><see href="https://libsodium.gitbook.io/doc/generating_random_data" /></summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void randombytes_buf(byte* buf, uint size);

        /// <summary><see href="https://libsodium.gitbook.io/doc/password_hashing/default_phf#password-storage" /></summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int crypto_pwhash_str(byte* @out, byte* passwd, long passwdlen, long opsLimit, int memLimit);

        /// <summary><see href="https://libsodium.gitbook.io/doc/password_hashing/default_phf#password-storage" /></summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int crypto_pwhash_str_verify(byte* @in, byte* passwd, long passwdlen);
    }
}
