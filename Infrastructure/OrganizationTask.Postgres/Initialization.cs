using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace OrganizationTask.Postgres;

/// <summary>
/// Класс инициализации контекста БД и данных.
/// </summary>
public static class Initialization
{
	/// <summary>
	/// Метод инициализации контекста БД и данных.
	/// </summary>
	/// <param name="serviceProvider">Провайдер сервисов приложения.</param>
	/// <returns>Возвращает объект <see cref="IServiceProvider"/> провайдера сервисов приложения.</returns>
	public static IServiceProvider InitContextAndData(this IServiceProvider serviceProvider)
	{
		using var scope = serviceProvider.CreateScope();
		var services = scope.ServiceProvider;

		try
		{
			var dbContext = services.GetRequiredService<OrganizationTaskContext>();
			if (dbContext.Database.EnsureCreated())
			{
				#region Data

				dbContext.Database.ExecuteSqlRaw(@"
BEGIN;

INSERT INTO public.calculation_metering_devices(start_year, end_year)
	VALUES ('2018', '2020'),
	('2018', '2021'),
	('2019', '2022');

INSERT INTO public.organizations(id, name, address)
	VALUES (1, 'ООО Транснефтьэнерго', 'Москва, Пресненская набережная, 4с2');

INSERT INTO public.daughter_organizations(id, name, address, organization_id)
	VALUES (1, 'Дочерняя организация', 'Москва, Пресненская набережная, 4с1', 1);

INSERT INTO public.object_of_demands(id, name, address, expired_period, daughter_organization_id)
	VALUES (1, 'ПС 110/10 Весна', 'Тестовый адрес', interval '365 days', 1);

INSERT INTO public.current_transformers(""number"", transformation_coefficient, check_date, current_transformer_type)
	VALUES (1, 1.5, TIMESTAMP WITH TIME ZONE '2024-10-19 10:23:54+02', 0),
		(2, 1.3, TIMESTAMP WITH TIME ZONE '2023-10-19 10:23:54+02', 1),
		(3, 1.1, TIMESTAMP WITH TIME ZONE '2022-10-19 10:23:54+02', 0);

INSERT INTO public.electric_energy_meters(""number"", calculation_type, check_date)
	VALUES (1, 0, TIMESTAMP WITH TIME ZONE '2020-10-19 10:23:54+02'),
		(2, 1, TIMESTAMP WITH TIME ZONE '2019-10-19 10:23:54+02'),
		(3, 1, TIMESTAMP WITH TIME ZONE '2018-10-19 10:23:54+02');

INSERT INTO public.voltage_transformers(""number"", transformation_coefficient, check_date, voltage_transformer_type)
	VALUES (1, 2.5, TIMESTAMP WITH TIME ZONE '2021-10-19 10:23:54+02', 1),
		(2, 2.4, TIMESTAMP WITH TIME ZONE '2020-10-19 10:23:54+02', 0),
		(3, 2.3, TIMESTAMP WITH TIME ZONE '2019-10-19 10:23:54+02', 1);

COMMIT");

				#endregion
			}
		}
		catch (Exception ex)
		{
			var logger = scope.ServiceProvider.GetRequiredService<ILogger<OrganizationTaskContext>>();
			logger.LogError(ex, $"An error occurred while initializing the database {nameof(OrganizationTaskContext)}.");
		}

		return serviceProvider;
	}
}
