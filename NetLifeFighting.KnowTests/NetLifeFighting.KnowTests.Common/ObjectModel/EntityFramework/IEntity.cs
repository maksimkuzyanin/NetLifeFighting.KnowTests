using System.Collections.Generic;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	/// <summary>
	/// Общий интерфейс для сущности
	/// </summary>
	public interface IEntity
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		int Id { get; set; }
	}

	public class Entity : IEntity
	{
		public Entity()
		{
		}

		public Entity(int id)
		{
			Id = id;
		}

		#region IdEqualityComparer

		private sealed class IdEqualityComparer : IEqualityComparer<Entity>
		{
			public bool Equals(Entity x, Entity y)
			{
				if (ReferenceEquals(x, y))
				{
					return true;
				}
				if (ReferenceEquals(x, null))
				{
					return false;
				}
				if (ReferenceEquals(y, null))
				{
					return false;
				}
				if (x.GetType() != y.GetType())
				{
					return false;
				}
				return x.Id == y.Id;
			}

			public int GetHashCode(Entity obj)
			{
				return obj.Id;
			}
		}

		private static readonly IEqualityComparer<Entity> IdComparerInstance = new IdEqualityComparer();

		public static IEqualityComparer<Entity> IdComparer
		{
			get { return IdComparerInstance; }
		}

		#endregion

		/// <summary>
		/// Идентификатор
		/// </summary>
		public virtual int Id { get; set; }

		#region Equality members

		protected bool Equals(Entity other)
		{
			if (Id == 0 && other.Id == 0)
			{
				//не инициализированные сущности, поэтому если ссылки различаются - то сущности считаем различными
				return false;
			}
			return Id == other.Id;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}
			if (ReferenceEquals(this, obj))
			{
				return true;
			}
			if (obj.GetType() != GetType())
			{
				return false;
			}
			return Equals((Entity)obj);
		}

		public override int GetHashCode()
		{
			return Id;
		}

		#endregion
	}

	public interface INamedEntity : IEntity
	{
		string Name { get; set; }
	}
}
