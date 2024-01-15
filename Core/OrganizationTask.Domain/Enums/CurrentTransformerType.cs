namespace OrganizationTask.Domain.Enums;

/// <summary>
/// Тип трансформатора тока.
/// </summary>
public enum CurrentTransformerType
{
	/// <summary>
	/// Обмоточный.
	/// </summary>
	Winding = 0,

	/// <summary>
	/// Тороидальный.
	/// </summary>
	Toroidal = 1,

	/// <summary>
	/// Стержневой.
	/// </summary>
	Core = 2
}
