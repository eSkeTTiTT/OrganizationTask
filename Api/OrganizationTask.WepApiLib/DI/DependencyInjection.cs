using Microsoft.Extensions.DependencyInjection;
using Organization.Task.App.DI;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OrganizationTask.WepApiLib.DI;

/// <summary>
/// Регистрация слоя WebApiLib.
/// </summary>
public static class DependencyInjection
{
	/// <summary>
	/// Метод добавления комментариев слоя WebApiLib.
	/// </summary>
	/// <param name="options">Настройки сваггера.</param>
	/// <returns>Возвращает настройки сваггера с добавленными комментариями слоя WebApiLib.</returns>
	public static SwaggerGenOptions AddWebApiLibComments(this SwaggerGenOptions options)
	{
		var xmlWebApiLib = $"{typeof(DependencyInjection).Assembly.GetName().Name}.xml";
		options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlWebApiLib));

		var xmlApp = $"{typeof(Organization.Task.App.AssemblyReference).Assembly.GetName().Name}.xml";
		options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlApp));

		var xmlDomain = $"{typeof(Domain.AssemblyReference).Assembly.GetName().Name}.xml";
		options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlDomain));

		options.CustomSchemaIds(x => x.FullName);

		return options;
	}

	/// <summary>
	/// Метод регистрации слоя WebApiLib.
	/// </summary>
	/// <param name="services">Коллекция сервисов приложения.</param>
	/// <returns>Возвращает обновленную коллекцию сервисов приложения.</returns>
	public static IServiceCollection AddWebApiLib(this IServiceCollection services)
	{
		services
			.AddControllers()
			.AddApplicationPart(typeof(DependencyInjection).Assembly);

		services.AddApp();

		return services;
	}
}
