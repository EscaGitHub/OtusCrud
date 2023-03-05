using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using OtusCrud.Data;
using OtusCrud.Data.DataModels;

namespace Otus.Auth.Repositories;

/// <summary>
/// <inheritdoc/>
/// </summary>
public class RegistrationRepository : IRegistrationRepository
{
    private readonly UserDataContext _context;

    /// <summary>
    /// Конструктор <see cref="RegistrationRepository"/>
    /// </summary>
    /// <param name="context"></param>
    public RegistrationRepository(UserDataContext context)
    {
        _context = context;
    }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async ValueTask<long> RegisterAsync(string login, string password, CancellationToken cancellationToken)
    {
        if (await _context.User.AnyAsync(t => string.Equals(t.UserName, login), cancellationToken))
        {
            throw new ValidationException("Невозможно создать пользователя. Пользователь уже существует.");
        }
        
        var entity = await _context.User.AddAsync(new UserEntity
        {
            UserName = login,
            Password = password
        }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Entity.Id;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async ValueTask<long?> FoundUserByCredentialsAsync(string login, string password, CancellationToken cancellationToken)
    {
        var user = await _context.User
            .FirstOrDefaultAsync(t => 
                    string.Equals(t.UserName, login) &&
                    string.Equals(t.Password, password), 
                cancellationToken);

        return user?.Id;
    }
}