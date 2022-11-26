using Microsoft.EntityFrameworkCore.ChangeTracking;
using OtusCrud.Data.DataModels;

namespace OtusCrud.Repositories;

/// <summary>
/// Интерфейс взаимодействия с пользователя.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Полчение пользователей.
    /// </summary>
    Task<List<UserEntity>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Получение пользователя.
    /// </summary>
    Task<UserEntity?> GetAsync(long id, CancellationToken cancellationToken);

    /// <summary>
    /// Добавление пользователя.
    /// </summary>
    /// <returns></returns>
    ValueTask<UserEntity> AddAsync(UserEntity entity, CancellationToken cancellationToken);

    /// <summary>
    /// Удаление пользователя.
    /// </summary>
    /// <returns></returns>
    Task DeleteAsync(long id, CancellationToken cancellationToken);

    /// <summary>
    /// Обновление пользователя.
    /// </summary>
    Task<UserEntity?> Update(UserEntity entity, CancellationToken cancellationToken);
}