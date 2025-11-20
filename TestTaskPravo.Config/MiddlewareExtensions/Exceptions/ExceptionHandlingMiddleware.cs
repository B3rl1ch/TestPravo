using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TestTaskPravo.Core.Exceptions.Models;

namespace TestTaskPravo.Config.MiddlewareExtensions.Exceptions;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly string? _logLevel;

    public ExceptionHandlingMiddleware(
        RequestDelegate next, IConfiguration configuration,
        ILogger<ExceptionHandlingMiddleware> logger
        )
    {
        _next = next;
        _logger = logger;
        _logLevel = configuration["Exception:Level"];
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BaseAppException ex)
        {
            _logger.LogWarning(ex, "Base App Exception caught");
            await WriteError(context, ex.Message, ex.ErrorCode, ex.StackTrace, HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception caught");
            await WriteError(context, "Internal server error", "internal_error",  ex.StackTrace, HttpStatusCode.InternalServerError);
        }
    }
    
    private Task WriteError(
        HttpContext context,
        string message,
        string code,
        string stackTrace,
        HttpStatusCode status)
    {
        if (context.Response.HasStarted)
            throw new InvalidOperationException("Response already started");

        context.Response.Clear();
        context.Response.StatusCode = (int)status;
        context.Response.ContentType = "application/json";

        var error = GetErrorResponse(context, message, code, stackTrace, status);
        var json = JsonConvert.SerializeObject(error);

        return context.Response.WriteAsync(json);
    }

    private ErrorResponse GetErrorResponse(HttpContext context,
        string message,
        string code,
        string stackTrace,
        HttpStatusCode status)
    {
        ErrorResponse error;

        switch (_logLevel)
        {
            case "Debug":
                error = new ErrorResponseDevelopment()
                {
                    Status = (int)status,
                    Code = code,
                    Message = message,
                    TraceId = context.TraceIdentifier,
                    StackTrace = stackTrace
                };
                break;
            
            default: 
                error = new ErrorResponse
                {
                    Status = (int)status,
                    Code = code,
                    Message = message,
                    TraceId = context.TraceIdentifier
                };
                break;
        }

        return error;
    } 
    
}