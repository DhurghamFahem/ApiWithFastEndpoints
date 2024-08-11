global using FastEndpoints;
using FastEndpoints.Swagger;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseAuthorization();
app.UseFastEndpoints(c =>
{
    c.Endpoints.RoutePrefix = "api";
    c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

app.UseSwaggerGen();

app.Run();
