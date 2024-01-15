using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organization.Task.App.Transactions.Factory;
using OrganizationTask.Domain.Exceptions;
using OrganizationTask.Postgres;
using OrganizationTask.WepApiLib.Contracts;

namespace OrganizationTask.WepApiLib.Controllers;

/// <summary>
/// Контроллер для работы с расчетными приборами учета.
/// </summary>
[ApiController]
[Route("metering-device")]
public sealed class CalculationMeteringDeviceController : ControllerBase
{
	#region Compile queries

	/// <summary>
	/// Асинхронный компилируемый запрос.
	/// Получить коллекцию объектов <see cref="CalculationMeteringDeviceDto"/> всех расчетных приборов учета в указанном году.
	/// </summary>
	private static Func<OrganizationTaskContext, string, IAsyncEnumerable<CalculationMeteringDeviceDto>> CompileAsyncQuery_GetCalculationMeteringDevicesDtoByYear =>
		EF.CompileAsyncQuery((OrganizationTaskContext context, string year) =>
			from device in context.CalculationMeteringDevices
			where device.StartYear == year
			select new CalculationMeteringDeviceDto(device.Id, device.StartYear, device.EndYear));

	#endregion

	/// <summary>
	/// Максимальная длина года.
	/// </summary>
	private const int YearMaxLength = 4;

	/// <summary>
	/// Контекст основной БД.
	/// </summary>
	private readonly OrganizationTaskContext _organizationTaskContext;

	/// <summary>
	/// Фабрика создания транзакций.
	/// </summary>
	private readonly ITransactionFactory _transactionFactory;

	/// <summary>
	/// Конструктор.
	/// </summary>
	/// <param name="organizationTaskContext">Контекст основной БД.</param>
	/// <param name="transactionFactory">Фабрика создания транзакций.</param>
	/// 
	public CalculationMeteringDeviceController(
		OrganizationTaskContext organizationTaskContext,
		ITransactionFactory transactionFactory)
	{
		_organizationTaskContext = organizationTaskContext;
		_transactionFactory = transactionFactory;
	}

	/// <summary>
	/// Запрос на получение всех расчетных приборов учета в указанном году.
	/// </summary>
	/// <param name="year">Указанный год выборки.</param>
	/// <param name="cancellationToken">Токен на отмену операции.</param>
	/// <returns>Возвращает коллекцию всех расчетных приборов учета в указанном году.</returns>
	[HttpGet]
	[ProducesResponseType(typeof(IReadOnlyCollection<CalculationMeteringDeviceDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetCalculationMeteringDevices(string year, CancellationToken cancellationToken)
	{
		if (string.IsNullOrEmpty(year))
			throw new BadRequestException($"Получение всех расчетных приборов учета в указанном году: параметр {nameof(year)} пуст или не задан.");

		if (year.Length == 0 || year.Length > YearMaxLength)
			throw new BadRequestException($"Получение всех расчетных приборов учета в указанном году: параметр {nameof(year)} имеет некорректную длину.");

		// Транзакционный режим
		using var scope = _transactionFactory.CreateTransactionScope();

		IReadOnlyCollection<CalculationMeteringDeviceDto> result;
		try
		{
			result = (await CompileAsyncQuery_GetCalculationMeteringDevicesDtoByYear(_organizationTaskContext, year)
				.ToListAsync(cancellationToken))
				.AsReadOnly();
		}
		catch (Exception ex)
		{
			scope.Dispose();
			throw new Exception("Получение всех расчетных приборов учета в указанном году: ошибка считывания данных из БД.", ex);
		}

		// Завершаем транзакцию
		scope.Complete();
		return Ok(result);
	}
}
