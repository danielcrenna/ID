namespace ID.Internal
{
    public abstract class Sodium
    {
        static Sodium() => Libsodium.sodium_init();
    }
}