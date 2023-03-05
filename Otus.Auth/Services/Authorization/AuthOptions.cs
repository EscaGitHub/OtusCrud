using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Otus.Auth.Services.Authorization;

/// <summary>
/// Настройки генерации токена JWT.
/// </summary>
public static class AuthOptions
{
    /// <summary>
    /// Издатель токена.
    /// </summary>
    public const string Issuer = "OtusAuthService";
    
    /// <summary>
    /// Потребитель токена.
    /// </summary>
    public const string Audience = "OtusAuthClient"; 
    
    /// <summary>
    /// Ключ для шифрования.
    /// </summary>
    private const string Key = "security_token654321";
    
    /// <summary>
    /// Симметричный ключ шифрования.
    /// </summary>
    /// <returns></returns>
    public static SymmetricSecurityKey GetSymmetricSecurityKey() => new (Encoding.UTF8.GetBytes(Key));
}