global using FastEndpoints;
using ApiWithFastEndpoints.Data;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddFastEndpoints()
                .SwaggerDocument();

var app = builder.Build();
app.UseFastEndpoints(c =>
{
    c.Endpoints.RoutePrefix = "api";
    c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

app.UseSwaggerGen();

await MigrateDatabaseAsync();

app.Run();

async Task MigrateDatabaseAsync()
{
    using var scope = app.Services.CreateScope();
    using var context = scope.ServiceProvider.GetService<AppDbContext>();
    await context!.Database.MigrateAsync();
}
