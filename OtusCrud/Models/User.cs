namespace OtusCrud.Models;

/// <summary>
/// User model.
/// </summary>
public record User
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// User name.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// First name.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Last name.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Email.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Phone.
    /// </summary>
    public string? Phone { get; set; }
}