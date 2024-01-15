using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrganizationTask.Domain.Models;
using OrganizationTask.Postgres.Extensions;

namespace OrganizationTask.Postgres.Configurations;

/// <summary>
/// Конфигурация модели <see cref="ElectricitySupplyPoint"/>.
/// </summary>
public sealed class ElectricitySupplyPointConfiguration : IEntityTypeConfiguration<ElectricitySupplyPoint>
{
	/// <summary>
	/// Метод конфигурации модели <see cref="ElectricitySupplyPoint"/>.
	/// </summary>
	/// <param name="builder">Строитель конфигурации.</param>
	public void Configure(EntityTypeBuilder<ElectricitySupplyPoint> builder)
	{
		builder.HasKey(e => e.Id);

		builder
			.HasOne(e => e.ObjectOfDemand)
			.WithMany(o => o.ElectricitySupplyPoints)
			.HasForeignKey(e => e.ObjectOfDemandId);

		builder
			.Property(e => e.NameOfPoint)
			.IsRequired()
			.HasMaxLength(50);

		builder
			.Property(e => e.MaxPower)
			.IsRequired();

		builder.HasDescription();
	}
}
