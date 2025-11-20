using Microsoft.AspNetCore.Builder;
using TestTaskPravo.Config.MiddlewareExtensions.Exceptions;

namespace TestTaskPravo.Config;

public static class MiddlewareConfigurationExtension
{
    public static IApplicationBuilder ConfigureMiddlewares(this IApplicationBuilder app)
    {
        app.UseExceptionMiddleware();
        
        return app;
    }
}