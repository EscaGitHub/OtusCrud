using System.Collections.Concurrent;
using OtusCrud.Models;

namespace OtusCrud.Services;

/// <summary>
/// <inheritdoc cref="IUserService"/>
/// </summary>
public class UserService : IUserService
{
    #region Initialize users
    private readonly Dictionary<long, User> _usersCollection = new()
    {
        {
            0,
            new User
            {
                Id = 0,
                UserName = "Iv",
                FirstName = "Ivan",
                LastName = "Ivanov",
                Email = "ivan@ivanov.ru",
                Phone = "79109009090"
            }
        },
        {
            1,
            new User
            {
                Id = 1,
                UserName = "Si",
                FirstName = "Petr",
                LastName = "Sidorov",
                Email = "petr@sidorov.ru",
                Phone = "79119009090"
            }
        }
    };
    #endregion

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task<ICollection<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await Task.FromResult(_usersCollection.Values);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task<User?> GetUserAsync(long userId, CancellationToken cancellationToken)
    {
        _usersCollection.TryGetValue(userId, out var user);

        return await Task.FromResult(user);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task<User> CreateUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Task DeleteUserAsync(long userId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Task<User> UpdateUserAsync(long userId)
    {
        throw new NotImplementedException();
    }
}