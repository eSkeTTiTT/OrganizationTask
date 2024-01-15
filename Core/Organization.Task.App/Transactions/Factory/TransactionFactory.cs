﻿using System.Transactions;

namespace Organization.Task.App.Transactions.Factory;

/// <summary>
/// Фабрика создания транзакций. Реализует интерфейс <see cref="ITransactionFactory"/>.
/// </summary>
public sealed class TransactionFactory : ITransactionFactory
{
	/// <summary>
	/// Метод создания объекта транзакции <see cref="TransactionScope"/>.
	/// </summary>
	/// <param name="scopeOption">Предоставляет дополнительные опции для создания области транзакции.</param>
	/// <param name="isolationLevel">Определяет уровень изоляции транзакции.</param>
	/// <param name="asyncFlowOption">Указывает, включена ли поддержка асинхронных операций.</param>
	/// <returns>Возвращает объект транзакции <see cref="TransactionScope"/>.</returns>
	public TransactionScope CreateTransactionScope(
		TransactionScopeOption scopeOption = TransactionScopeOption.RequiresNew,
		IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
		TransactionScopeAsyncFlowOption asyncFlowOption = TransactionScopeAsyncFlowOption.Enabled) =>
		new(
			scopeOption: scopeOption,
			transactionOptions: new TransactionOptions { IsolationLevel = isolationLevel },
			asyncFlowOption: asyncFlowOption);
}
