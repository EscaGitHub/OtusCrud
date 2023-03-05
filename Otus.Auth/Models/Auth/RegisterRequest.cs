using System.ComponentModel.DataAnnotations;

namespace Otus.Auth.Models.Auth;

/// <summary>
/// Запрос регистрации пользователя.
/// </summary>
/// <param name="Login">Логин.</param>
/// <param name="Password">Пароль.</param>
public record RegisterRequest([Required]string Login, [Required, MinLength(5)]string Password);