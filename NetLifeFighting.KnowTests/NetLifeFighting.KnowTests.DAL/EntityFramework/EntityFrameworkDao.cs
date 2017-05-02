using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using EntityFramework.BulkInsert.Extensions;
using NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework;
using NetLifeFighting.KnowTests.DAL.Common;

namespace NetLifeFighting.KnowTests.DAL.EntityFramework
{
	/// <summary>
	/// Базовый класс доступа к данным через EntityFramework
	/// </summary>
	public abstract class EntityFrameworkDao
	{
		private TestsContext _context;

		protected EntityFrameworkDao()
		{
		}

		protected EntityFrameworkDao(TestsContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Получение текущего контекста
		/// </summary>
		protected TestsContext Context
		{
			get
			{
				if (_context == null)
				{
					_context = new TestsContext();
				}
				return _context;
			}
		}
	}

	/// <summary>
	/// Базовый обобщенный класс доступа к данным сущности конкретного типа
	/// </summary>
	/// <typeparam name="T">Тип сущности</typeparam>
	public class EntityFrameworkDao<T> : EntityFrameworkDao, IEntityDao<T>
		where T : class
	{
		private volatile IDbSet<T> _entities;
		
		/// <summary>
		/// множество объектов одного типа
		/// </summary>
		private IDbSet<T> Entities
		{
			get
			{
				if (_entities == null)
				{
					_entities = Context.Set<T>();
				}
				return _entities;
			}
		}

		public EntityFrameworkDao()
		{
		}

		public EntityFrameworkDao(TestsContext context)
			: base(context)
		{
		}

		public T GetById(int id)
		{
			return Entities.Find(id);
		}

		public List<T> GetByIds(params int[] ids)
		{
			if (!typeof(IEntity).IsAssignableFrom(typeof(T)))
			{
				throw new NotSupportedException("supported only for IEntity types");
			}

			return Entities
				.Where(x => ids.Contains(((IEntity) x).Id))
				.ToList();
		}

		public void Save(object item)
		{
			MarkToSaveOrUpdate(item);
			Context.SaveChanges();
		}

		public void Save(IEnumerable<object> items)
		{
			foreach (var item in items)
			{
				MarkToSaveOrUpdate(item);
			}
			Context.SaveChanges();
		}

		IEnumerable<object> IEntityDao.GetAll()
		{
			return Query.ToList();
		}

		public bool Exists<TEntity>(int id)
			where TEntity: class, IEntity
		{
			return Context.Set<TEntity>().Any(x => x.Id == id);
		}

		public void Save(T item)
		{
			MarkToSaveOrUpdate(item);
			Context.SaveChanges();
		}

		public void Save(IEnumerable<T> items)
		{
			foreach (var item in items)
			{
				MarkToSaveOrUpdate(item);
			}
			Context.SaveChanges();

			//Context.BulkInsert(items);
		}

		private void MarkToSaveOrUpdate(T item)
		{
			DbEntityEntry<T> entry = Context.Entry(item);
			if (entry.State == EntityState.Detached)
			{
				Entities.Add(item);
			}
			else
			{
				entry.State = EntityState.Modified;
			}
		}

		private void MarkToSaveOrUpdate(object item)
		{
			MarkToSaveOrUpdate((T) item);
		}

		object IEntityDao.GetById(int id)
		{
			return GetById(id);
		}

		public IEnumerable<T> GetAll()
		{
			return Query.ToList();
		}

		/// <summary>
		/// Удаляет объект
		/// </summary>
		/// <param name="item"></param>
		public virtual void Delete(T item)
		{
			var entry = Context.Entry(item);
			entry.State = EntityState.Deleted;
			Entities.Remove(item);
		}

		/// <summary>
		/// Удаляет объекты
		/// </summary>
		/// <param name="items"></param>
		public virtual void Delete(params T[] items)
		{
			foreach (var item in items)
			{
				var entry = Context.Entry(item);
				entry.State = EntityState.Deleted;
				Entities.Remove(item);
			}
		}

		/// <summary>
		/// Для запросов внутри Dao-классов
		/// </summary>
		protected IQueryable<T> Query
		{
			get
			{
				return Entities;
			}
		}
	}
}
