namespace OrganizationTask.WepApiLib.Requests;

/// <summary>
/// Контекст запроса на добавление новой точки измерения.
/// </summary>
public sealed class AddMeasurementPointRequest
{
	/// <summary>
	/// Наименование новой точки измерения.
	/// </summary>
	public string Name { get; set; } = null!;

	/// <summary>
	/// Идентификатор объекта потребления.
	/// </summary>
	public int ObjectOfDemandId { get; set; }

	/// <summary>
	/// Идентификатор трансформатора тока.
	/// </summary>
	public int CurrentTransformerId { get; set; }

	/// <summary>
	/// Идентификатор счетчика электрической энергии.
	/// </summary>
	public int ElectricEnergyMeterId { get; set; }

	/// <summary>
	/// Идентификатор трансформатора напряжения.
	/// </summary>
	public int VoltageTransformerId { get; set; }
}
