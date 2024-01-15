namespace OrganizationTask.Domain.Exceptions;

/// <summary>
/// Ошибка данных запроса.
/// </summary>
public class BadRequestException : ApplicationException
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="message">Сообщение об ошибке.</param>
    public BadRequestException(string message)
        : base("Bad Request", message)
    {
    }
}