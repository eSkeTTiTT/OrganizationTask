using OrganizationTask.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace OrganizationTask.Domain.Models;

/// <summary>
/// Трансформатор напряжения.
/// </summary>
public sealed class VoltageTransformer
{
	/// <summary>
	/// Номер.
	/// </summary>
	[Key]
	public int Number { get; set; }

	/// <summary>
	/// КТН (коэффициент трансформации).
	/// </summary>
	public double TransformationCoefficient { get; set; }

	/// <summary>
	/// Дата проверки.
	/// </summary>
	public DateTime CheckDate { get; set; }

	/// <summary>
	/// Тип трансформатора напряжения.
	/// </summary>
	public VoltageTransformerType VoltageTransformerType { get; set; }

	/// <summary>
	/// Точка измерения электроэнергии.
	/// </summary>
	public ElectricityMeasurementPoint? ElectricityMeasurementPoint { get; set; }
}
