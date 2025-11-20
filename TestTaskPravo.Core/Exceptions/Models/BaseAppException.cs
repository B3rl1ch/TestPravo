namespace TestTaskPravo.Core.Exceptions.Models;

public class BaseAppException : Exception
{
    public string ErrorCode { get; }

    public BaseAppException(string message, string errorCode)
        : base(message)
    {
        ErrorCode = errorCode;
    }
}