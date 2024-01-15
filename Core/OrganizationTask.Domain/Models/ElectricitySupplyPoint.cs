using System.ComponentModel;

namespace OrganizationTask.Domain.Models;

/// <summary>
/// Точка поставки электроэнергии.
/// </summary>
public sealed class ElectricitySupplyPoint
{
	/// <summary>
	/// Идентификатор записи.
	/// </summary>
	[Description("Идентификатор")]
	public int Id { get; set; }

	/// <summary>
	/// Наименование точки поставки.
	/// </summary>
	[Description("Наименование точки поставки")]
	public string NameOfPoint { get; set; } = null!;

	/// <summary>
	/// Максимальная мощность, кВт.
	/// </summary>
	[Description("Максимальная мощность, кВт")]
	public int MaxPower { get; set; }

	/// <summary>
	/// Идентификатор объекта потребления.
	/// </summary>
	public int? ObjectOfDemandId { get; set; }

	/// <summary>
	/// Объект потребления.
	/// </summary>
	public ObjectOfDemand? ObjectOfDemand { get; set; }
}
