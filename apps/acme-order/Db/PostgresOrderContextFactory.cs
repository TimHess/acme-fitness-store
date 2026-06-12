using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Steeltoe.Connectors.PostgreSql;

namespace AcmeOrder.Db;

// Enables `dotnet ef migrations add` without a running Postgres instance.
// Mirrors the production DI setup so Steeltoe reads the connection string
// from Steeltoe:Client:PostgreSql:Default:ConnectionString in appsettings.Development.json.
public class PostgresOrderContextFactory : IDesignTimeDbContextFactory<PostgresOrderContext>
{
    public PostgresOrderContext CreateDbContext(string[] args)
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            Args = args,
            EnvironmentName = Environments.Development
        });
        builder.AddPostgreSql();
        builder.Services.AddDbContext<PostgresOrderContext>();

        var app = builder.Build();
        return app.Services.CreateScope().ServiceProvider.GetRequiredService<PostgresOrderContext>();
    }
}
