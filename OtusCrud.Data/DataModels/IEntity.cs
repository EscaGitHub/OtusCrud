namespace OtusCrud.Data.DataModels;

/// <summary>
/// Интерфейс базововй сущности.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Идентификтаор сущности.
    /// </summary>
    long Id { get; set; }
}