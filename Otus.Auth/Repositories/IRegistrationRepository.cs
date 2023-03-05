namespace Otus.Auth.Repositories;

/// <summary>
/// Интерфейс регистрации пользователя.
/// </summary>
public interface IRegistrationRepository
{
    /// <summary>
    /// Регистрация.
    /// </summary>
    /// <returns></returns>
    ValueTask<long> RegisterAsync(string login, string password, CancellationToken cancellationToken);
    
    /// <summary>
    /// Проверить наличие пользователя по логину и паролю.
    /// </summary>
    /// <returns></returns>
    ValueTask<long?> FoundUserByCredentialsAsync(string login, string password, CancellationToken cancellationToken);
}