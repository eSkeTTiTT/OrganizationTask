using System.ComponentModel;

namespace OrganizationTask.Domain.Models;

/// <summary>
/// Дочерняя организация.
/// </summary>
public class DaughterOrganization
{
	/// <summary>
	/// Идентификатор записи.
	/// </summary>
	[Description("Идентифкатор")]
	public int Id { get; set; }

	/// <summary>
	/// Наименование.
	/// </summary>
	[Description("Наименование")]
	public string Name { get; set; } = null!;

	/// <summary>
	/// Адрес.
	/// </summary>
	[Description("Адрес")]
	public string Address { get; set; } = null!;

	/// <summary>
	/// Идентификатор организации.
	/// </summary>
	public int? OrganizationId { get; set; }

	/// <summary>
	/// Организация.
	/// </summary>
	public Organization? Organization { get; set; }

	/// <summary>
	/// Объекты потребления.
	/// </summary>
	public virtual ICollection<ObjectOfDemand>? ObjectOfDemands { get; set; }
}
