using OrganizationTask.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace OrganizationTask.Domain.Models;

/// <summary>
/// Счетчик электрической энергии.
/// </summary>
public sealed class ElectricEnergyMeter
{
	/// <summary>
	/// Номер.
	/// </summary>
	[Key]
	public int Number { get; set; }

	/// <summary>
	/// Тип счетчика.
	/// </summary>
	public CalculationType CalculationType { get; set; }

	/// <summary>
	/// Дата проверки.
	/// </summary>
	public DateTime CheckDate { get; set; }

	/// <summary>
	/// Точка измерения электроэнергии.
	/// </summary>
	public ElectricityMeasurementPoint? ElectricityMeasurementPoint { get; set; }
}
