namespace OrganizationTask.Domain.Models;

/// <summary>
/// Расчетный прибор учета.
/// </summary>
public class CalculationMeteringDevice
{
	/// <summary>
	/// Идентификатор записи.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Год начала расчета.
	/// </summary>
	public string StartYear { get; set; } = null!;

	/// <summary>
	/// Год конца расчета.
	/// </summary>
	public string EndYear { get; set; } = null!;

	/// <summary>
	/// Точки измерения электроэнергии.
	/// </summary>
	public virtual ICollection<ElectricityMeasurementPoint>? ElectricityMeasurementPoints { get; set; }
}
