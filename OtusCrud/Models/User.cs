namespace OtusCrud.Models;

/// <summary>
/// User model.
/// </summary>
public record User : UserModel
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public long Id { get; set; }
}