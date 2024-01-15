namespace OrganizationTask.Domain.Models;

/// <summary>
/// Организация.
/// </summary>
public class Organization
{
	/// <summary>
	/// Идентификатор записи.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Наименование.
	/// </summary>
	public string Name { get; set; } = null!;

	/// <summary>
	/// Адрес.
	/// </summary>
	public string Address { get; set; } = null!;

	/// <summary>
	/// Дочерние организации.
	/// </summary>
	public virtual ICollection<DaughterOrganization>? DaughterOrganizations { get; set; }
}
