using System.ComponentModel;

namespace OrganizationTask.Domain.Models;

/// <summary>
/// Точка измерения электроэнергии.
/// </summary>
public class ElectricityMeasurementPoint
{
	/// <summary>
	/// Идентификатор записи.
	/// </summary>
	[Description("Идентификатор")]
	public int Id { get; set; }

	/// <summary>
	/// Наименование.
	/// </summary>
	[Description("Наименование")]
	public string Name { get; set; } = null!;

	/// <summary>
	/// Идентификатор объекта потребления.
	/// </summary>
	public int? ObjectOfDemandId { get; set; }

	/// <summary>
	/// Объект потребления.
	/// </summary>
	public ObjectOfDemand? ObjectOfDemand { get; set; }

	/// <summary>
	/// Идентификатор счетчика электрической энергии.
	/// </summary>
	public int? ElectricEnergyMeterId { get; set; }

	/// <summary>
	/// Счетчик электрической энергии.
	/// </summary>
	public virtual ElectricEnergyMeter? ElectricEnergyMeter { get; set; }

	/// <summary>
	/// Идентификатор трансформатора тока.
	/// </summary>
	public int? CurrentTransformerId { get; set; }

	/// <summary>
	/// Трансформатор тока.
	/// </summary>
	public virtual CurrentTransformer? CurrentTransformer { get; set; }

	/// <summary>
	/// Идентификатор трансформатора напряжения.
	/// </summary>
	public int? VoltageTransformerId { get; set; }

	/// <summary>
	/// Трансформатор напряжения.
	/// </summary>
	public virtual VoltageTransformer? VoltageTransformer { get; set; }

	/// <summary>
	/// Расчетные приборы учета.
	/// </summary>
	public virtual ICollection<CalculationMeteringDevice>? CalculationMeteringDevices { get; set; }
}
