using Microsoft.AspNetCore.Builder;

namespace TestTaskPravo.Config.MiddlewareExtensions.Exceptions;

public static class ExceptionMiddlewareExtension
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}