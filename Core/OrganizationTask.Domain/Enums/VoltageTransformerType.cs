namespace OrganizationTask.Domain.Enums;

/// <summary>
/// Тип трансформатора напряжения.
/// </summary>
public enum VoltageTransformerType
{
	/// <summary>
	/// Заземленный.
	/// </summary>
	Grounded = 0,

	/// <summary>
	/// Незаземленный.
	/// </summary>
	Ungrounded = 1,

	/// <summary>
	/// Каскадный.
	/// </summary>
	Cascade = 2,
}
