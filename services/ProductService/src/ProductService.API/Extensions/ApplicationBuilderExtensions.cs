using Microsoft.EntityFrameworkCore;
using ProductService.Infrastructure.Persistence;

namespace ProductService.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static WebApplication ApplyDatabaseMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ApplicationDbContext>();

        const int maxRetries = 10;

        for (int retry = 1; retry <= maxRetries; retry++)
        {
            try
            {
                Console.WriteLine($"Applying migrations... Attempt {retry}");

                context.Database.Migrate();

                Console.WriteLine("Database migration completed.");

                return app;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Migration failed: {ex.Message}");

                if (retry == maxRetries)
                    throw;

                Thread.Sleep(TimeSpan.FromSeconds(5));
            }
        }

        return app;
    }
}