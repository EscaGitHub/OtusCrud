namespace OtusCrud.Models;

/// <summary>
/// Base user model.
/// </summary>
public record UserModel
{
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