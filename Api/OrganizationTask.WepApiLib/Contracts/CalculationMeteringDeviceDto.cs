using OrganizationTask.Domain.Models;

namespace OrganizationTask.WepApiLib.Contracts;

/// <summary>
/// DTO расчетного прибора учета <see cref="CalculationMeteringDevice"/>.
/// </summary>
/// <param name="Id">Идентификатор записи.</param>
/// <param name="StartYear">Год начала расчета.</param>
/// <param name="EndYear">Год конца расчета.</param>
public sealed record CalculationMeteringDeviceDto(int Id, string StartYear, string EndYear);
