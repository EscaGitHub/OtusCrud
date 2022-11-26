using OtusCrud.Models;

namespace OtusCrud.Services;

/// <summary>
/// Сервис управления пользователями.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Получить всех пользователей. 
    /// </summary>
    /// <returns></returns>
    Task<ICollection<User>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Получить пользователя.
    /// </summary>
    /// <returns></returns>
    Task<User?> GetUserAsync(long userId, CancellationToken cancellationToken);

    /// <summary>
    /// Создать пользователя.
    /// </summary>
    Task<User> CreateUserAsync(UserModel user, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удалить пользователя.
    /// </summary>
    Task DeleteUserAsync(long userId, CancellationToken cancellationToken);

    /// <summary>
    /// Обновить пользователя.
    /// </summary>
    Task<User?> UpdateUserAsync(long userId, UserModel user, CancellationToken cancellationToken);
}