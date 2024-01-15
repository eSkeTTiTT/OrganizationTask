using System.ComponentModel;

namespace OrganizationTask.Domain.Models;

/// <summary>
/// Объект потребления.
/// </summary>
public class ObjectOfDemand
{
	/// <summary>
	/// Идентификатор записи.
	/// </summary>
	[Description("Идентификатор")]
	public int Id { get; set; }

	/// <summary>
	/// Наименование объекта.
	/// </summary>
	[Description("Наименование объекта")]
	public string Name { get; set; } = null!;

	/// <summary>
	/// Адрес.
	/// </summary>
	[Description("Адрес")]
	public string Address { get; set; } = null!;

	/// <summary>
	/// Срок поверки.
	/// !!! PostgreSQL intervals with month or year components cannot be read as TimeSpan. Consider using NodaTime's Period type, or NpgsqlInterval.
	/// </summary>
	[Description("Срок поверки")]	
	public TimeSpan ExpiredPeriod { get; set; }

	/// <summary>
	/// Идентифкатор дочерней организации.
	/// </summary>
	public int? DaughterOrganizationId { get; set; }

	/// <summary>
	/// Дочерняя организация.
	/// </summary>
	public DaughterOrganization? DaughterOrganization { get; set; }

	/// <summary>
	/// Точки измерения электроэнергии.
	/// </summary>
	public virtual ICollection<ElectricityMeasurementPoint>? ElectricityMeasurementPoints { get; set; }

	/// <summary>
	/// Точки поставки электроэнергии.
	/// </summary>
	public virtual ICollection<ElectricitySupplyPoint>? ElectricitySupplyPoints { get; set; }
}
