using Microsoft.EntityFrameworkCore;
using OrganizationTask.Domain.Models;
using System.Diagnostics;

namespace OrganizationTask.Postgres;

/// <summary>
/// Контекст основной БД.
/// </summary>
public sealed class OrganizationTaskContext : DbContext
{
	#region Properties

	/// <summary>
	/// Организации.
	/// </summary>
	public DbSet<Organization> Organizations { get; set; }

	/// <summary>
	/// Дочернии организации.
	/// </summary>
	public DbSet<DaughterOrganization> DaughterOrganizations { get; set; }

	/// <summary>
	/// Объекты потребления.
	/// </summary>
	public DbSet<ObjectOfDemand> ObjectOfDemands { get; set; }

	/// <summary>
	/// Точки измерения электроэнергии.
	/// </summary>
	public DbSet<ElectricityMeasurementPoint> ElectricityMeasurementPoints { get; set; }

	/// <summary>
	/// Точки поставки электроэнергии.
	/// </summary>
	public DbSet<ElectricitySupplyPoint> ElectricitySupplyPoints { get; set; }

	/// <summary>
	/// Счетчики электрической энергии.
	/// </summary>
	public DbSet<ElectricEnergyMeter> ElectricEnergyMeters { get; set; }

	/// <summary>
	/// Трансформаторы тока.
	/// </summary>
	public DbSet<CurrentTransformer> CurrentTransformers { get; set; }

	/// <summary>
	/// Трансформаторы напряжения.
	/// </summary>
	public DbSet<VoltageTransformer> VoltageTransformers { get; set; }

	/// <summary>
	/// Расчетные приборы учета.
	/// </summary>
	public DbSet<CalculationMeteringDevice> CalculationMeteringDevices { get; set; }

	#endregion

	/// <summary>
	/// Конструктор.
	/// </summary>
	/// <param name="options">Настройки подключения.</param>
	public OrganizationTaskContext(DbContextOptions<OrganizationTaskContext> options)
        : base(options)
    {
    }

	/// <summary>
	/// Метод конфигурирования контекста БД.
	/// </summary>
	/// <param name="optionsBuilder">Строитель конфигурации.</param>
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
		optionsBuilder
			.LogTo(s => Trace.WriteLine(s))
			.EnableServiceProviderCaching(true)
			.EnableSensitiveDataLogging(true)
			.EnableDetailedErrors(true);

	/// <summary>
	/// Метод настройки конфигурации модели БД.
	/// </summary>
	/// <param name="modelBuilder">Строитель модели.</param>
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder
			.Entity<CurrentTransformer>()
			.HasOne(c => c.ElectricityMeasurementPoint)
			.WithOne(e => e.CurrentTransformer)
			.HasForeignKey<ElectricityMeasurementPoint>(e => e.CurrentTransformerId);

		modelBuilder
			.Entity<VoltageTransformer>()
			.HasOne(v => v.ElectricityMeasurementPoint)
			.WithOne(e => e.VoltageTransformer)
			.HasForeignKey<ElectricityMeasurementPoint>(e => e.CurrentTransformerId);

		modelBuilder
			.Entity<ElectricEnergyMeter>()
			.HasOne(e => e.ElectricityMeasurementPoint)
			.WithOne(e => e.ElectricEnergyMeter)
			.HasForeignKey<ElectricityMeasurementPoint>(e => e.CurrentTransformerId);

		modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrganizationTaskContext).Assembly);
	}
}
