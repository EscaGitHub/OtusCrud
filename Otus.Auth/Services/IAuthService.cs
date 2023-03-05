namespace Otus.Auth.Services;

/// <summary>
/// Интерфейс сервиса аутентификации.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Зарешистрировать пользователя.
    /// </summary>
    /// <returns></returns>
    ValueTask<long> RegisterUserAsync(string login, string password, CancellationToken cancellationToken);

    /// <summary>
    /// Получить токен доступа пользователя.
    /// </summary>
    /// <returns></returns>
    Task<string> GetUserTokenAsync(string login, string password, CancellationToken cancellationToken);
}