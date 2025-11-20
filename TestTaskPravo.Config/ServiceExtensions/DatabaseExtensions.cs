using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTaskPravo.Data.Database;

namespace TestTaskPravo.Config.ServiceExtensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConfig = configuration.GetSection("Database");
        var connectionString = configuration.GetConnectionString("Default") ??
            $"Host={dbConfig["Host"]};" +
            $"Port={dbConfig["Port"]};" +
            $"Database={dbConfig["Database"]};" +
            $"Username={dbConfig["Username"]};" +
            $"Password={dbConfig["Password"]}";
        
        Console.WriteLine($"DB connection: {connectionString}");
        
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
        
        return services;
    }
}