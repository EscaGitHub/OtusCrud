using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OtusCrud.Data;
using OtusCrud.Data.DataModels;
using OtusCrud.Models;

namespace OtusCrud.Repositories;

/// <summary>
/// <inheritdoc/>
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly UserDataContext _context;

    /// <summary>
    /// Конструктор <see cref="UserRepository"/>
    /// </summary>
    /// <param name="context"></param>
    public UserRepository(UserDataContext context)
    {
        _context = context;
    }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Task<List<UserEntity>> GetAllAsync()
    {
        return _context.User.ToListAsync();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task<UserEntity?> GetAsync(long id, CancellationToken cancellationToken)
    {
        var user = await _context.User.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);

        return user;
    }
    

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async ValueTask<UserEntity> AddAsync(UserEntity entity, CancellationToken cancellationToken)
    {
        var userEntity = await _context.User.AddAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return userEntity.Entity;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task DeleteAsync(long id)
    {
        var user = await _context.User.FindAsync(id);

        if (user is not null)
        {
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task<UserEntity?> Update(UserEntity entity)
    {
        var user = await _context.User.FindAsync(entity.Id);

        if (user != null)
        {
            user.FirstName = entity.FirstName;
            user.LastName = entity.LastName;
            user.Phone = entity.Phone;
            user.Email = entity.Email;

            await _context.SaveChangesAsync();

            return user;
        }

        return null;
    }
}