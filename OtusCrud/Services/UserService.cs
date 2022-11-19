using System.Collections.Concurrent;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OtusCrud.Data.DataModels;
using OtusCrud.Models;
using OtusCrud.Repositories;

namespace OtusCrud.Services;

/// <summary>
/// <inheritdoc cref="IUserService"/>
/// </summary>
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
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
        var userDto = await _userRepository.GetAsync(userId, cancellationToken);

        return userDto is null ? null : Convert(userDto);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task<User> CreateUserAsync(User user, CancellationToken cancellationToken)
    {
        var userDto = await _userRepository.AddAsync(Convert(user), cancellationToken);

        return Convert(userDto);
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

    private User Convert(UserEntity userDto)
    {
        return new User
        {
            Id = userDto.Id,
            UserName = userDto.UserName,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Phone = userDto.Phone,
            Email = userDto.Email
        };
    }

    private UserEntity Convert(User user)
    {
        return new UserEntity
        {
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Phone = user.Phone,
            Email = user.Email
        };
    }
}