namespace OrganizationTask.Domain.Exceptions;

/// <summary>
/// Ошибка приложения.
/// </summary>
public abstract class ApplicationException : Exception
{
	/// <summary>
	/// Конструктор.
	/// </summary>
	/// <param name="title">Заголовок.</param>
	/// <param name="message">Сообщение об ошибке.</param>
	public ApplicationException(string title, string message)
        : base(message) =>
        Title = title;

    /// <summary>
    /// Заголовок.
    /// </summary>
    public string Title { get; }
}
