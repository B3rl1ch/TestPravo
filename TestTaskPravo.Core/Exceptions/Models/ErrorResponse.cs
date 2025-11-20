namespace TestTaskPravo.Core.Exceptions.Models;

public class ErrorResponse
{
    public int Status { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string TraceId { get; set; } = string.Empty;
}

public sealed class ErrorResponseDevelopment : ErrorResponse
{
    public string StackTrace { get; set; } = string.Empty;
}