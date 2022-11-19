namespace OtusCrud.Data.DataModels;

/// <summary>
/// Базовая модель.
/// </summary>
public class BaseEntity : IEntity
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public long Id { get; set; }
}