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

    /// <summary>
    /// Конструктор <see cref="UserService"/>
    /// </summary>
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task<ICollection<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        var usersDto =  await _userRepository.GetAllAsync(cancellationToken);

        var users = usersDto.Select(Convert).ToArray();

        return users;
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
    public async Task<User> CreateUserAsync(UserModel user, CancellationToken cancellationToken)
    {
        var userDto = await _userRepository.AddAsync(Convert(user), cancellationToken);

        return Convert(userDto);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Task DeleteUserAsync(long userId, CancellationToken cancellationToken)
    {
        return _userRepository.DeleteAsync(userId, cancellationToken);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task<User?> UpdateUserAsync(long userId, UserModel user, CancellationToken cancellationToken)
    {
        var userEntity = Convert(user);
        userEntity.Id = userId;
        
        var userDto = await _userRepository.Update(userEntity, cancellationToken);
        
        return userDto is null ? null : Convert(userDto);
    }

    #region Private methods
    
    private static User Convert(UserEntity userDto)
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

    private static UserEntity Convert(UserModel user)
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
    
    #endregion
    
}