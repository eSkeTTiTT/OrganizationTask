using OrganizationTask.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace OrganizationTask.Domain.Models;

/// <summary>
/// Трансформатор тока.
/// </summary>
public sealed class CurrentTransformer
{
	/// <summary>
	/// Номер.
	/// </summary>
	[Key]
	public int Number { get; set; }

	/// <summary>
	/// КТТ (коэффициент трансформации).
	/// </summary>
	public double TransformationCoefficient { get; set; }

	/// <summary>
	/// Дата проверки.
	/// </summary>
	public DateTime CheckDate { get; set; }

	/// <summary>
	/// Тип трансформатора тока.
	/// </summary>
	public CurrentTransformerType CurrentTransformerType { get; set; }

	/// <summary>
	/// Точка измерения электроэнергии.
	/// </summary>
	public ElectricityMeasurementPoint? ElectricityMeasurementPoint { get; set; }
}
