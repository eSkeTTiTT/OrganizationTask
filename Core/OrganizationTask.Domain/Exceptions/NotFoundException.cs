namespace OrganizationTask.Domain.Exceptions;

/// <summary>
/// Ошибка отсутствия сущности.
/// </summary>
public class NotFoundException : ApplicationException
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="message">Сообщение об ошибке.</param>
    public NotFoundException(string message)
        : base("Not Found", message)
    {
    }
}