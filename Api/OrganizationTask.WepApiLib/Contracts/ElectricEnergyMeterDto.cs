using OrganizationTask.Domain.Enums;

namespace OrganizationTask.WepApiLib.Contracts;

/// <summary>
/// DTO счетчика электрической энергии.
/// </summary>
/// <param name="Number">Номер.</param>
/// <param name="CalculationType">Тип счетчика.</param>
/// <param name="CheckDate">Дата проверки.</param>
public sealed record class ElectricEnergyMeterDto(int Number, CalculationType CalculationType, DateTime CheckDate);
