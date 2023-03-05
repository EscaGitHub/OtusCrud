using System.Security.Authentication;
using Otus.Auth.Repositories;

namespace Otus.Auth.Services;

/// <summary>
/// Сервис аутентификации.
/// </summary>
public class AuthService : IAuthService
{
    private readonly IRegistrationRepository _registrationRepository;

    /// <summary>
    /// Конструктор <see cref="AuthService"/>
    /// </summary>
    public AuthService(IRegistrationRepository registrationRepository)
    {
        _registrationRepository = registrationRepository;
    }
    
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async ValueTask<long> RegisterUserAsync(string login, string password, CancellationToken cancellationToken)
    {
        return await _registrationRepository.RegisterAsync(login, password, cancellationToken);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task<string> GetUserTokenAsync(string login, string password, CancellationToken cancellationToken)
    {
        var userId = await _registrationRepository.FoundUserByCredentialsAsync(login, password, cancellationToken);
        
        if (!userId.HasValue)
        {
            throw new InvalidOperationException("Пароль или логин неверны.");
        }

        var token = Jwt.GenerateToken(login, userId.Value, cancellationToken);

        return token;
    }
}