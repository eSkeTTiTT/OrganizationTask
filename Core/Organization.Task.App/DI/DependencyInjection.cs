using Microsoft.Extensions.DependencyInjection;
using Organization.Task.App.Transactions.Factory;

namespace Organization.Task.App.DI;

/// <summary>
/// Регистрация слоя App.
/// </summary>
public static class DependencyInjection
{
	/// <summary>
	/// Метод регистрации слоя App.
	/// </summary>
	/// <param name="services">Коллекция сервисов приложения.</param>
	/// <returns>Возвращает обновленную коллекцию сервисов приложения.</returns>
	public static IServiceCollection AddApp(this IServiceCollection services) =>
		services.AddSingleton<ITransactionFactory, TransactionFactory>();
}
