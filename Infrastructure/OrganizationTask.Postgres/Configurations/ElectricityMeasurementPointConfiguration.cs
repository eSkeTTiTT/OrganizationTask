using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrganizationTask.Domain.Models;
using OrganizationTask.Postgres.Extensions;

namespace OrganizationTask.Postgres.Configurations;

/// <summary>
/// Конфигурация модели <see cref="ElectricityMeasurementPoint"/>.
/// </summary>
public sealed class ElectricityMeasurementPointConfiguration : IEntityTypeConfiguration<ElectricityMeasurementPoint>
{
	/// <summary>
	/// Метод конфигурации модели <see cref="ElectricityMeasurementPoint"/>.
	/// </summary>
	/// <param name="builder">Строитель конфигурации.</param>
	public void Configure(EntityTypeBuilder<ElectricityMeasurementPoint> builder)
	{
		builder.HasKey(e => e.Id);

		builder
			.HasOne(e => e.ObjectOfDemand)
			.WithMany(o => o.ElectricityMeasurementPoints)
			.HasForeignKey(e => e.ObjectOfDemandId);

		builder
			.HasMany(e => e.CalculationMeteringDevices)
			.WithMany(c => c.ElectricityMeasurementPoints)
			.UsingEntity(
				l => l.HasOne(typeof(CalculationMeteringDevice)).WithMany().HasPrincipalKey(nameof(CalculationMeteringDevice.StartYear), nameof(CalculationMeteringDevice.EndYear)),
				r => r.HasOne(typeof(ElectricityMeasurementPoint)).WithMany().HasPrincipalKey(nameof(ElectricityMeasurementPoint.Id))) ;

		builder
			.Property(e => e.Name)
			.IsRequired()
			.HasMaxLength(50);

		builder.HasDescription();
	}
}
