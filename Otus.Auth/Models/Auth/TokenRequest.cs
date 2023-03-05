using System.ComponentModel.DataAnnotations;

namespace Otus.Auth.Models.Auth;

/// <summary>
/// Получение токена доступа.
/// </summary>
/// <param name="Login">Логин.</param>
/// <param name="Password">Пароль.</param>
public record TokenRequest([Required]string Login, [Required]string Password);