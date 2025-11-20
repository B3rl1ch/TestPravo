using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTaskPravo.Config.ServiceExtensions;
using TestTaskPravo.Core.Abstractions;
using TestTaskPravo.Core.Database.Abstraction;
using TestTaskPravo.Core.Database.Abstraction.Providers;
using TestTaskPravo.Core.Services;
using TestTaskPravo.Data.Database.Abstraction;
using TestTaskPravo.Data.Providers;

namespace TestTaskPravo.Config;

/// <summary>
/// Базовый класс для настройки конфигурации
/// </summary>
public static class ServiceConfigurationExtension
{
    /// <summary>
    /// Базовый метод для расширения сервисов
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        
        return services;
    }
    
    /// <summary>
    /// Базовый метод для настройки зависимостей
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
    {   
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IArticleProvider, ArticleProvider>();
        services.AddScoped<ISectionProvider, SectionProvider>();
        services.AddScoped<ITagProvider, TagProvider>();
        
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<ISectionService, SectionService>();
        services.AddScoped<ITagService, TagService>();
        
        return services;
    }
}