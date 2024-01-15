using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OrganizationTask.Domain.Exceptions;
using System.Text.Json;
using ApplicationException = OrganizationTask.Domain.Exceptions.ApplicationException;

namespace OrganizationTask.WepApiLib.Middleware;

/// <summary>
/// Перехват исключений.
/// </summary>
public sealed class ExceptionHandlingMiddleware : IMiddleware
{
    /// <summary>
    /// Логгер.
    /// </summary>
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

	/// <summary>
	/// Конструктор - Перехват исключений.
	/// </summary>
	/// <param name="logger">Логгер.</param>
	public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) =>
        _logger = logger;

	/// <summary>
	/// Invoke.
	/// </summary>
	/// <param name="context">Контекст запроса.</param>
	/// <param name="next">Следующий делегат в конвейере.</param>
	/// <returns>Возвращает объект текущей задачи <see cref="Task"/>.</returns>
	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
	        _logger.LogError("{Error}", ex);

	        await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Обработчик ошибки.
    /// </summary>
    /// <param name="httpContext">Контекст запроса.</param>
    /// <param name="exception">Объект ошибки.</param>
    /// <returns>Возвращает объект текущей задачи <see cref="Task"/>.</returns>
    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);

        var response = new
        {
            title = GetTitle(exception),
            status = statusCode,
            detail = exception.Message
        };

        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

	/// <summary>
	/// Метод получения HTTP кода по исключению.
	/// </summary>
	/// <param name="exception">Объект исключения.</param>
	/// <returns>Возвращает HTTP код по исключению.</returns>
	private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            NoAccessRightException => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };

	/// <summary>
	/// Метод получения заголовка исключения.
	/// </summary>
	/// <param name="exception">Объект исключения.</param>
	/// <returns>Возвращает заголовок исключения.</returns>
	private static string GetTitle(Exception exception) =>
        exception switch
        {
            ApplicationException applicationException => applicationException.Title,
            _ => "Server Error"
        };
}
