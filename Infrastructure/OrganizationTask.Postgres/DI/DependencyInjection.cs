using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OrganizationTask.Postgres.DI;

/// <summary>
/// Регистрация модуля для работы БД.
/// </summary>
public static class DependencyInjection
{
	/// <summary>
	/// Метод регистрации модуля для работы БД.
	/// </summary>
	/// <param name="services">Коллекция сервисов приложения.</param>
	/// <param name="configuration">Объект конфигурации приложения.</param>
	/// <returns>Возвращает обновленную коллекцию сервисов приложения.</returns>
	public static IServiceCollection AddPersistenceNpgDb(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<OrganizationTaskContext>(options =>
			options
				.UseNpgsql(configuration.GetConnectionString("pgConnectionString"))
				.UseSnakeCaseNamingConvention());

		// Разрешить не UTC даты
		AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

		return services;
	}

	/// <summary>
	/// Метод регистрации модуля для работы БД.
	/// </summary>
	/// <param name="services">Коллекция сервисов приложения.</param>
	/// <param name="connectionString">Строка подключения к БД.</param>
	/// <returns>Возвращает обновленную коллекцию сервисов приложения.</returns>
	public static IServiceCollection AddPersistenceNpgDbConnectionString(this IServiceCollection services, string connectionString)
	{
		services.AddDbContext<OrganizationTaskContext>(options =>
			options
				.UseNpgsql(connectionString)
				.UseSnakeCaseNamingConvention());

		// Разрешить не UTC даты
		AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

		return services;
	}
}
