using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OtusCrud.Data;
using OtusCrud.Data.DataModels;

var config = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var optionsBuilder = new DbContextOptionsBuilder<UserDataContext>();

var connectionString = config.GetConnectionString("OtusUsers");

Console.WriteLine($"Connection string: {connectionString}");

optionsBuilder.UseNpgsql(connectionString);

await using var db = new UserDataContext(optionsBuilder.Options);

Console.WriteLine("Run migrations ...");
await db.Database.MigrateAsync();
Console.WriteLine("Migrations added.");

if (!db.User.Any())
{
    // Add default users:
    Console.WriteLine("Adding default users...");

    await db.AddRangeAsync(new List<object>
    {
        new UserEntity
        {
            Email = "ivanov@ivanov.ru",
            FirstName = "Ivan",
            LastName = "Ivanov",
            Phone = "70001000000",
            UserName = "IvanI"
        },
        new UserEntity
        {
            Email = "petrov@petrov.ru",
            FirstName = "Petr",
            LastName = "Petrov",
            Phone = "70002000000",
            UserName = "PetrP"           
        }
    });
    
    await db.SaveChangesAsync();
    
    Console.WriteLine("Users added.");
}

