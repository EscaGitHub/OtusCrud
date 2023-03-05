using Otus.Auth.Models.Account;

namespace Otus.Auth.Services;

/// <summary>
/// Сервис работы с аккаунтом пользователя.
/// </summary>
public interface IAccountService
{
    /// <summary>
    /// Получить информацию о пользователе.
    /// </summary>
    Task<UserModel?> GetAsync(long userId, CancellationToken cancellationToken);

    /// <summary>
    /// обновить пользователя.
    /// </summary>
    Task<long?> UpdateAsync(long userId, UserModel user, CancellationToken cancellationToken);
}