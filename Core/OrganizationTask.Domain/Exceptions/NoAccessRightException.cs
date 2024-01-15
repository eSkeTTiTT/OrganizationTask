namespace OrganizationTask.Domain.Exceptions;

/// <summary>
/// Ошибка прав доступа.
/// </summary>
public class NoAccessRightException : ApplicationException
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="message">Сообщение об ошибке.</param>
    public NoAccessRightException(string message)
        : base("No access", message)
    {
    }
}
