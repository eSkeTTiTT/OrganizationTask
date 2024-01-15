using OrganizationTask.Domain.Enums;
using OrganizationTask.Domain.Models;

namespace OrganizationTask.WepApiLib.Contracts;

/// <summary>
/// DTO трансформатора напряжения <see cref="VoltageTransformer"/>.
/// </summary>
/// <param name="Number">Номер.</param>
/// <param name="TransformationCoefficient">КТН (коэффициент трансформации).</param>
/// <param name="CheckDate">Дата проверки.</param>
/// <param name="VoltageTransformerType">Тип трансформатора напряжения.</param>
public sealed record VoltageTransformerDto(int Number, double TransformationCoefficient, DateTime CheckDate, VoltageTransformerType VoltageTransformerType);
