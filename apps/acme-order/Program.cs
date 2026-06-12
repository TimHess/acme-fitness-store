using System;
using System.Net.Http.Headers;
using AcmeOrder.Db;
using AcmeOrder.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Steeltoe.Configuration.CloudFoundry;
using Steeltoe.Configuration.CloudFoundry.ServiceBindings;
using Steeltoe.Connectors.PostgreSql;
using Steeltoe.Discovery.Eureka;
using Steeltoe.Discovery.HttpClients;
using Steeltoe.Management.Endpoint.Actuators.All;

var builder = WebApplication.CreateBuilder(args);
builder.AddCloudFoundryConfiguration();
builder.Configuration.AddCloudFoundryServiceBindings();
builder.Services.AddAllActuators();
builder.Services.AddEurekaDiscoveryClient();

switch (builder.Configuration["DatabaseProvider"])
{
    case "Sqlite":
        builder.Services.AddDbContext<OrderContext, SqliteOrderContext>();
        break;

    case "Postgres":
        builder.AddPostgreSql();
        builder.Services.AddDbContext<OrderContext, PostgresOrderContext>();
        break;
}

builder.Services.AddHttpClient<OrderService>(c =>
    {
        c.BaseAddress = new Uri("https://acme-payment");
        c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    })
    .AddServiceDiscovery();
builder.Services.AddControllers();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        if (builder.Configuration["DisableTokenValidation"] == "true")
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                SignatureValidator = delegate (string token, TokenValidationParameters _)
                {
                    var jwt = new JsonWebToken(token);
                    return jwt;
                }
            };
        }
    });

builder.Services.AddAuthorization();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var orderContext = scope.ServiceProvider.GetRequiredService<OrderContext>();
    await orderContext.Database.MigrateAsync();
}

app.UseDeveloperExceptionPage();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
