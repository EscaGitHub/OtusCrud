using Otus.Auth.Models;
using System.Reflection;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Otus.Auth.Repositories;
using Otus.Auth.Services;
using Otus.Auth.Services.Authorization;
using OtusCrud.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(ConfigureJwtBearer);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddProblemDetails(ConfigureProblemDetails);

builder.Services.AddDbContext<UserDataContext>(option =>
    option.UseNpgsql(builder.Configuration.GetConnectionString("OtusUsers")));

// DI:
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

var app = builder.Build();

app.UseProblemDetails();

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/health", () => new HealthResponse { Status = "OK" });

app.Run();


#region Private

void ConfigureProblemDetails(ProblemDetailsOptions options)
{
    options.IncludeExceptionDetails = (_, _) =>
    {
        var variable = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        
        return variable is not null && variable == "Development";
    };
}

void ConfigureJwtBearer(JwtBearerOptions options)
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // указывает, будет ли валидироваться издатель при валидации токена
        ValidateIssuer = true,
        // строка, представляющая издателя
        ValidIssuer = AuthOptions.Issuer,
        // будет ли валидироваться потребитель токена
        ValidateAudience = true,
        // установка потребителя токена
        ValidAudience = AuthOptions.Audience,
        // будет ли валидироваться время существования
        ValidateLifetime = true,
        // установка ключа безопасности
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
        // валидация ключа безопасности
        ValidateIssuerSigningKey = true,
    };
}

#endregion


