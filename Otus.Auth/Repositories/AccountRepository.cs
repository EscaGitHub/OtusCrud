using OtusCrud.Data;
using OtusCrud.Data.DataModels;

namespace Otus.Auth.Repositories;

/// <summary>
/// <inheritdoc/>
/// </summary>
public class AccountRepository : IAccountRepository
{
    private readonly UserDataContext _context;

    /// <summary>
    /// Конструктор <see cref="AccountRepository"/>
    /// </summary>
    public AccountRepository(UserDataContext context)
    {
        _context = context;
    }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task<UserEntity?> GetAsync(long userId, CancellationToken cancellationToken)
    {
        return await _context.User.FindAsync(new object?[] { userId }, cancellationToken);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task<long?> UpdateAsync(long userId, UserEntity userEntity, CancellationToken cancellationToken)
    {
        var user = await _context.User.FindAsync(new object?[] { userId }, cancellationToken);

        if (user is null) return null;
        
        user.FirstName = userEntity.FirstName;
        user.LastName = userEntity.LastName;
        user.Phone = userEntity.Phone;
        user.Email = userEntity.Email;
            
        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}