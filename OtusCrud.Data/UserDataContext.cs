using Microsoft.EntityFrameworkCore;
using OtusCrud.Data.DataModels;

namespace OtusCrud.Data;

/// <summary>
/// Контекст взаимодействия с БД.
/// </summary>
public class UserDataContext : DbContext
{
    /// <summary>
    /// Конструктор <see cref="UserDataContext"/>
    /// </summary>
    /// <param name="options"></param>
    public UserDataContext(DbContextOptions<UserDataContext> options) : base(options)
    {
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
    }
    
    /// <summary>
    /// Пользователи.
    /// </summary>
    public DbSet<UserEntity> User { get; set; }
}