using System.Collections.Generic;

namespace NetLifeFighting.KnowTests.DAL.Common
{
	/// <summary>
	/// Интерфейс базового типизированного Dao
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IEntityDao<T>: IEntityDao
	{
		T GetById(int id);

		void Save(T item);

		void Save(IEnumerable<T> items);

		IEnumerable<T> GetAll();
	}

	/// <summary>
	/// Интерфейс базового Dao
	/// </summary>
	public interface IEntityDao
	{
		/// <summary>
		/// Получает объект по идентификатору
		/// </summary>
		/// <param name="id">Идентификатор</param>
		/// <returns></returns>
		object GetById(int id);

		/// <summary>
		/// Сохраняет объект
		/// </summary>
		/// <param name="item"></param>
		void Save(object item);

		/// <summary>
		/// Сохраняет объекты
		/// </summary>
		/// <param name="items"></param>
		void Save(IEnumerable<object> items);

		/// <summary>
		/// Получает все объекты типа
		/// </summary>
		/// <returns></returns>
		IEnumerable<object> GetAll();
	}
}
