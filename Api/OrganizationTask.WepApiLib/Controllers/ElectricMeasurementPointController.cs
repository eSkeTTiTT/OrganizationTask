using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organization.Task.App.Transactions.Factory;
using OrganizationTask.Domain.Exceptions;
using OrganizationTask.Postgres;
using OrganizationTask.WepApiLib.Requests;

namespace OrganizationTask.WepApiLib.Controllers;

/// <summary>
/// Контроллер для работы с точками измерения электроэнергии.
/// </summary>
[ApiController]
[Route("measurement-point")]
public sealed class ElectricMeasurementPointController : ControllerBase
{
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
	public ElectricMeasurementPointController(
		OrganizationTaskContext organizationTaskContext,
		ITransactionFactory transactionFactory)
	{
		_organizationTaskContext = organizationTaskContext;
		_transactionFactory = transactionFactory;
	}

	/// <summary>
	/// Запрос на добавление новой точки измерения.
	/// </summary>
	/// <param name="request">Контекст запроса.</param>
	/// <param name="cancellationToken">Токен на отмену операции.</param>
	/// <returns>Возвращает признак успешности операции.</returns>
	[HttpPost]
	[Route("add")]
	[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
	public async Task<IActionResult> AddMeasurementPoint(AddMeasurementPointRequest request, CancellationToken cancellationToken)
	{
		if (request is null)
			throw new BadRequestException("Добавление новой точки измерения: объект запроса не задан.");

		if (string.IsNullOrEmpty(request.Name))
			throw new BadRequestException("Добавление новой точки измерения: некорректное наименование новой точки.");

		// Транзакционный режим
		using var scope = _transactionFactory.CreateTransactionScope();

		try
		{
			_organizationTaskContext.ElectricityMeasurementPoints.Add(new()
			{
				Name = request.Name,
				ObjectOfDemandId = request.ObjectOfDemandId,
				CurrentTransformerId = request.CurrentTransformerId,
				VoltageTransformerId = request.VoltageTransformerId,
				ElectricEnergyMeterId = request.ElectricEnergyMeterId
			});

			// Сохраняем изменения
			await _organizationTaskContext.SaveChangesAsync(cancellationToken);
		}
		catch (Exception)
		{
			scope.Dispose();
			throw new BadRequestException("Ошибка при добавлении новой точки измерения в БД.");
		}

		// Завершаем транзакцию
		scope.Complete();
		return Ok(true);
	}
}
