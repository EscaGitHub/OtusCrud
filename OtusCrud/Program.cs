using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OtusCrud.Data;
using OtusCrud.Models;
using OtusCrud.Repositories;
using OtusCrud.Services;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddDbContext<UserDataContext>(option =>
    option.UseNpgsql(builder.Configuration.GetConnectionString("OtusUsers")));

// DI:
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

app.UseRouting();
app.UseHttpMetrics();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoint =>
{
    endpoint.MapMetrics();
});

app.MapGet("/health", () => new HealthResponse { Status = "OK" });

app.Run();