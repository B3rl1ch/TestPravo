using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace TestTaskPravo.Config.ServiceExtensions;

public static class SerilogExtensions
{
    public static IHostBuilder UseSerilogLoggingExtension(this IHostBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(
                path: "../log/all-app-log-.log",
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: LogEventLevel.Error)
            .CreateLogger();

        return builder.UseSerilog();
    }
}