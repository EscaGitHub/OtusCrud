using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Otus.Auth.Services.Authorization;

namespace Otus.Auth.Services;

/// <summary>
/// Работа с JWT токенами.
/// </summary>
public static class Jwt
{
    /// <summary>
    /// Сгенерировать токен.
    /// </summary>
    /// <returns></returns>
    public static string GenerateToken(string login, long userId, CancellationToken cancellationToken)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, login),
            new(JwtRegisteredClaimNames.Sub, userId.ToString()), // добавили идентификатор пользователя
            new(JwtRegisteredClaimNames.Name, login)
        };

        // Создание JWT-токена:
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}