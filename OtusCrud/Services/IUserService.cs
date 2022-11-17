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
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ICollection<User>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Получить пользователя.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<User?> GetUserAsync(long userId, CancellationToken cancellationToken);

    /// <summary>
    /// Создать пользователя.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<User> CreateUserAsync(User user);
    
    /// <summary>
    /// Удалить пользователя.
    /// </summary>
    /// <param name="userId"></param>
    Task DeleteUserAsync(long userId);

    /// <summary>
    /// Обновление пользователя.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<User> UpdateUserAsync(long userId);
}