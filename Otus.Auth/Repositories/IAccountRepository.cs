using OtusCrud.Data.DataModels;

namespace Otus.Auth.Repositories;

/// <summary>
/// Работа с аккаунтом.
/// </summary>
public interface IAccountRepository
{
    /// <summary>
    /// Получить пользователя.
    /// </summary>
    Task<UserEntity?> GetAsync(long userId, CancellationToken cancellationToken);

    /// <summary>
    /// Обновить пользователя.
    /// </summary>
    Task<long?> UpdateAsync(long userId, UserEntity userEntity, CancellationToken cancellationToken);
}