using Microsoft.EntityFrameworkCore;
using TestTaskPravo.Config;
using TestTaskPravo.Config.ServiceExtensions;
using TestTaskPravo.Data.Database;
using TestTaskPravo.Data.Mapping;
using TestTaskPravo.Mapping;

namespace TestTaskPravo;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilogLoggingExtension();

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<DataMappingProfile>();
            cfg.AddProfile<ArticleMappingProfile>();
            cfg.AddProfile<SectionMappingProfile>();
        });
        
        builder.Services.ConfigureExtensions(builder.Configuration);
        builder.Services.ConfigureDependencies(builder.Configuration);
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.Use(async (context, next) =>
    {
        if (context.Request.Path == "/")
        {
            context.Response.Redirect("/swagger/index.html", permanent: false);
            return;
        }
        await next();
    });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate();
        }

        app.ConfigureMiddlewares();
        
        app.Run();
    }
}
