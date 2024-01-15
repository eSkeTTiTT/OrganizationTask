using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrganizationTask.Domain.Models;
using OrganizationTask.Postgres.Extensions;

namespace OrganizationTask.Postgres.Configurations;

/// <summary>
/// Конфигурация модели <see cref="ObjectOfDemand"/>.
/// </summary>
public sealed class ObjectOfDemandConfigurations : IEntityTypeConfiguration<ObjectOfDemand>
{
	/// <summary>
	/// Метод конфигурации модели <see cref="ObjectOfDemand"/>.
	/// </summary>
	/// <param name="builder">Строитель конфигурации.</param>
	public void Configure(EntityTypeBuilder<ObjectOfDemand> builder)
	{
		builder.HasKey(o => o.Id);

		builder
			.HasOne(o => o.DaughterOrganization)
			.WithMany(d => d.ObjectOfDemands)
			.HasForeignKey(o => o.DaughterOrganizationId);

		builder
			.Property(o => o.Name)
			.IsRequired()
			.HasMaxLength(255);

		builder
			.Property(o => o.Address)
			.IsRequired()
			.HasMaxLength(255);

		builder
			.Property(o => o.ExpiredPeriod)
			.IsRequired();

		builder.HasDescription();
	}
}
