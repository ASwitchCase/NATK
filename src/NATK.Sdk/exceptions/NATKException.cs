namespace NATK.Sdk.Exceptions;

/// <summary>
/// Base class for exceptions thrown by the NATK SDK.
/// </summary>
public class NATKException : Exception
{
    public NATKException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}