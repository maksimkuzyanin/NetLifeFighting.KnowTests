using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLifeFighting.KnowTests.Common.Abstraction.Result
{
	/// <summary>
	/// Статусы завершения выполнения процедур базы данных
	/// </summary>
	public enum ResultStatus
	{
		/// <summary>
		/// Операция выполнена успешно
		/// </summary>
		Success = 0,
		/// <summary>
		/// Конкурентный конфликт, необходимо обновить информацию
		/// </summary>
		ConcurrencyConflict = 1,
		/// <summary>
		/// Конфликт уникальности
		/// </summary>
		UniqueConflict = 2,
		/// <summary>
		/// Не корректные данные
		/// </summary>
		IncorrectData = 3,
		/// <summary>
		/// Не удалось выполнить операцию
		/// </summary>
		Failure = 4,
		/// <summary>
		/// Неизвестный код выполнения операции
		/// </summary>
		UnknownDbResultCode = 5,
		/// <summary>
		/// Невозможно удалить запись, так как имеются не удаленые зависимые записи
		/// </summary>
		HasUnclosedDependentObjects = 6,
		/// <summary>
		/// Результат выполнения операции неизвестен
		/// </summary>
		Unknown = 1024
	}
}
