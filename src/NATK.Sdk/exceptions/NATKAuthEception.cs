namespace NATK.Sdk.Exceptions;

public sealed class NATKAuthException : NATKException
{
    public NATKAuthException(Exception? innerException = null)
        : base("Failed to authenticate user credentials.", innerException)
    {
    }
}