namespace OtusCrud.Data.DataModels;

/// <summary>
/// User model.
/// </summary>
public class UserEntity : BaseEntity
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

    /// <summary>
    /// Password.
    /// </summary>
    public string? Password { get; set; }
}