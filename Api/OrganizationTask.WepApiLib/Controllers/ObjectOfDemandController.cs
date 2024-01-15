using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organization.Task.App.Transactions.Factory;
using OrganizationTask.Domain.Exceptions;
using OrganizationTask.Postgres;
using OrganizationTask.WepApiLib.Contracts;

namespace OrganizationTask.WepApiLib.Controllers;

/// <summary>
/// Контроллер для работы с объектами потребления.
/// </summary>
[ApiController]
[Route("object-of-demand")]
public sealed class ObjectOfDemandController : ControllerBase
{
	#region Compile queries

	/// <summary>
	/// Асинхронный компилируемый запрос.
	/// Получить коллекцию объектов <see cref="CurrentTransformerDto"/> всех трансформаторов тока с закончившимся сроком поверке по указанному объекту потребления.
	/// </summary>
	private static Func<OrganizationTaskContext, int, DateTime, TimeSpan, IAsyncEnumerable<CurrentTransformerDto>> CompileAsyncQuery_GetExpiredCurrentTransformersDto =>
		EF.CompileAsyncQuery((OrganizationTaskContext context, int id, DateTime now, TimeSpan expiredPeriod) =>
			from point in context.ElectricityMeasurementPoints
			where point.ObjectOfDemandId == id
			join transformer in context.CurrentTransformers on point.CurrentTransformerId equals transformer.Number
			where transformer.CheckDate + expiredPeriod < now
			select new CurrentTransformerDto(transformer.Number, transformer.TransformationCoefficient, transformer.CheckDate, transformer.CurrentTransformerType));

	/// <summary>
	/// Асинхронный компилируемый запрос.
	/// Получить коллекцию объектов <see cref="VoltageTransformerDto"/> всех трансформаторов напряжения с закончившимся сроком поверке по указанному объекту потребления.
	/// </summary>
	private static Func<OrganizationTaskContext, int, DateTime, TimeSpan, IAsyncEnumerable<VoltageTransformerDto>> CompileAsyncQuery_GetExpiredVoltageTransformersDto =>
		EF.CompileAsyncQuery((OrganizationTaskContext context, int id, DateTime now, TimeSpan expiredPeriod) =>
			from point in context.ElectricityMeasurementPoints
			where point.ObjectOfDemandId == id
			join transformer in context.VoltageTransformers on point.CurrentTransformerId equals transformer.Number
			where transformer.CheckDate + expiredPeriod < now
			select new VoltageTransformerDto(transformer.Number, transformer.TransformationCoefficient, transformer.CheckDate, transformer.VoltageTransformerType));

	/// <summary>
	/// Асинхронный компилируемый запрос.
	/// Получить коллекцию объектов <see cref="ElectricEnergyMeterDto"/> всех счетчиков с закончившимся сроком поверке по указанному объекту потребления.
	/// </summary>
	private static Func<OrganizationTaskContext, int, DateTime, TimeSpan, IAsyncEnumerable<ElectricEnergyMeterDto>> CompileAsyncQuery_GetExpiredElectricEnergyMetersDto =>
		EF.CompileAsyncQuery((OrganizationTaskContext context, int id, DateTime now, TimeSpan expiredPeriod) =>
			from point in context.ElectricityMeasurementPoints
			where point.ObjectOfDemandId == id
			join meter in context.ElectricEnergyMeters on point.CurrentTransformerId equals meter.Number
			where meter.CheckDate + expiredPeriod < now
			select new ElectricEnergyMeterDto(meter.Number, meter.CalculationType, meter.CheckDate));

	#endregion

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
	public ObjectOfDemandController(
		OrganizationTaskContext organizationTaskContext,
		ITransactionFactory transactionFactory)
	{
		_organizationTaskContext = organizationTaskContext;
		_transactionFactory = transactionFactory;
	}

	/// <summary>
	/// Запрос на получение всех трансформаторов тока с закончившимся сроком поверке по указанному объекту потребления.
	/// </summary>
	/// <param name="id">Идентификатор объекта потребления.</param>
	/// <param name="cancellationToken">Токен на отмену операции.</param>
	/// <returns>Возвращает коллекцию всех трансформаторов тока с закончившимся сроком поверке по указанному объекту потребления.</returns>
	[HttpGet]
	[Route("get-expired-current-transformers")]
	[ProducesResponseType(typeof(IReadOnlyCollection<CurrentTransformerDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetExpiredCurrentTransformers(int id, CancellationToken cancellationToken)
	{
		// Транзакционный режим
		using var scope = _transactionFactory.CreateTransactionScope();

		var objectOfDemand = await _organizationTaskContext.ObjectOfDemands.FirstOrDefaultAsync(v => v.Id == id, cancellationToken)
			?? throw new NotFoundException("Получение всех трансформаторов тока с закончившимся сроком поверке по указанному объекту потребления: объекта потребления с указанным идентификатором не существует."); ;

		IReadOnlyCollection<CurrentTransformerDto> result;
		try
		{
			result = (await CompileAsyncQuery_GetExpiredCurrentTransformersDto(_organizationTaskContext, id, DateTime.Now, objectOfDemand.ExpiredPeriod)
				.ToListAsync(cancellationToken))
				.AsReadOnly();
		}
		catch (Exception ex)
		{
			scope.Dispose();
			throw new Exception("Получение всех трансформаторов тока с закончившимся сроком поверке по указанному объекту потребления: ошибка считывания данных из БД.", ex);
		}

		// Завершаем транзакцию
		scope.Complete();
		return Ok(result);
	}

	/// <summary>
	/// Запрос на получение всех трансформаторов напряжения с закончившимся сроком поверке по указанному объекту потребления.
	/// </summary>
	/// <param name="id">Идентификатор объекта потребления.</param>
	/// <param name="cancellationToken">Токен на отмену операции.</param>
	/// <returns>Возвращает коллекцию всех трансформаторов напряжения с закончившимся сроком поверке по указанному объекту потребления.</returns>
	[HttpGet]
	[Route("get-expired-voltage-transformers")]
	[ProducesResponseType(typeof(IReadOnlyCollection<VoltageTransformerDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetExpiredVoltageTransformers(int id, CancellationToken cancellationToken)
	{
		// Транзакционный режим
		using var scope = _transactionFactory.CreateTransactionScope();

		var objectOfDemand = await _organizationTaskContext.ObjectOfDemands.FirstOrDefaultAsync(v => v.Id == id, cancellationToken)
			?? throw new NotFoundException("Получение всех трансформаторов напряжения с закончившимся сроком поверке по указанному объекту потребления: объекта потребления с указанным идентификатором не существует."); ;

		IReadOnlyCollection<VoltageTransformerDto> result;
		try
		{
			result = (await CompileAsyncQuery_GetExpiredVoltageTransformersDto(_organizationTaskContext, id, DateTime.Now, objectOfDemand.ExpiredPeriod)
				.ToListAsync(cancellationToken))
				.AsReadOnly();
		}
		catch (Exception ex)
		{
			scope.Dispose();
			throw new Exception("Получение всех трансформаторов напряжения с закончившимся сроком поверке по указанному объекту потребления: ошибка считывания данных из БД.", ex);
		}

		// Завершаем транзакцию
		scope.Complete();
		return Ok(result);
	}

	/// <summary>
	/// Запрос на получение всех счетчиков с закончившимся сроком поверке по указанному объекту потребления.
	/// </summary>
	/// <param name="id">Идентификатор объекта потребления.</param>
	/// <param name="cancellationToken">Токен на отмену операции.</param>
	/// <returns>Возвращает коллекцию всех счетчиков с закончившимся сроком поверке по указанному объекту потребления.</returns>
	[HttpGet]
	[Route("get-expired-electric-energy-meters")]
	[ProducesResponseType(typeof(IReadOnlyCollection<ElectricEnergyMeterDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetExpiredElectricEnergyMeters(int id, CancellationToken cancellationToken)
	{
		// Транзакционный режим
		using var scope = _transactionFactory.CreateTransactionScope();

		var objectOfDemand = await _organizationTaskContext.ObjectOfDemands.FirstOrDefaultAsync(v => v.Id == id, cancellationToken)
			?? throw new NotFoundException("Получение всех счетчиков с закончившимся сроком поверке по указанному объекту потребления: объекта потребления с указанным идентификатором не существует."); ;

		IReadOnlyCollection<ElectricEnergyMeterDto> result;
		try
		{
			result = (await CompileAsyncQuery_GetExpiredElectricEnergyMetersDto(_organizationTaskContext, id, DateTime.Now, objectOfDemand.ExpiredPeriod)
				.ToListAsync(cancellationToken))
				.AsReadOnly();
		}
		catch (Exception ex)
		{
			scope.Dispose();
			throw new Exception("Получение всех счетчиков с закончившимся сроком поверке по указанному объекту потребления: ошибка считывания данных из БД.", ex);
		}

		// Завершаем транзакцию
		scope.Complete();
		return Ok(result);
	}
}
