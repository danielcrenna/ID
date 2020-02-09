using System;

namespace ID.Internal
{
    internal static class BufferExtensions
    {
        public static void Fill(this Span<byte> data, uint size)
        {
            unsafe
            {
                fixed (byte* ptr = &data.GetPinnableReference())
                {
                    Libsodium.randombytes_buf(ptr, size);
                }
            }
        }
    }
}