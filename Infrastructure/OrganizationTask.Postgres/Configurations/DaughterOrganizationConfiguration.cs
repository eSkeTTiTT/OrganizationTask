using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrganizationTask.Domain.Models;
using OrganizationTask.Postgres.Extensions;

namespace OrganizationTask.Postgres.Configurations;

/// <summary>
/// Конфигурация модели <see cref="DaughterOrganization"/>.
/// </summary>
public sealed class DaughterOrganizationConfiguration : IEntityTypeConfiguration<DaughterOrganization>
{
	/// <summary>
	/// Метод конфигурации модели <see cref="DaughterOrganization"/>.
	/// </summary>
	/// <param name="builder">Строитель конфигурации.</param>
	public void Configure(EntityTypeBuilder<DaughterOrganization> builder)
	{
		builder.HasKey(d => d.Id);

		builder
			.HasOne(d => d.Organization)
			.WithMany(o => o.DaughterOrganizations)
			.HasForeignKey(d => d.OrganizationId);

		builder
			.Property(d => d.Name)
			.IsRequired()
			.HasMaxLength(255);

		builder
			.Property(d => d.Address)
			.IsRequired()
			.HasMaxLength(255);

		builder.HasDescription();
	}
}
