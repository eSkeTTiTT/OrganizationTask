using OrganizationTask.Domain.Enums;
using OrganizationTask.Domain.Models;

namespace OrganizationTask.WepApiLib.Contracts;

/// <summary>
/// DTO трансформатора тока <see cref="CurrentTransformer"/>.
/// </summary>
/// <param name="Number">Номер.</param>
/// <param name="TransformationCoefficient">КТТ (коэффициент трансформации).</param>
/// <param name="CheckDate">Дата проверки.</param>
/// <param name="CurrentTransformerType">Тип трансформатора напряжения.</param>
public record CurrentTransformerDto(int Number, double TransformationCoefficient, DateTime CheckDate, CurrentTransformerType CurrentTransformerType);
