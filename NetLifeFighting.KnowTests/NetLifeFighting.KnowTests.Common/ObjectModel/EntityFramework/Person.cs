using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	/// <summary>
	/// Пользователь
	/// </summary>
	[Table("Person")]
	public class Person : Entity
	{
		public Person()
		{
			//PersonAnswers = new HashSet<PersonAnswer>();
		}

		/// <summary>
		/// Идентификатор
		/// </summary>
		[Key]
		public virtual int PersonId { get; set; }

		/// <summary>
		/// Имя
		/// </summary>
		public virtual string Nickname { get; set; }

		/// <summary>
		/// Пароль
		/// </summary>
		public virtual string Password { get; set; }

		/// <summary>
		/// Ответы пользователя
		/// </summary>
		//public virtual ISet<PersonAnswer> PersonAnswers { get; set; }

		public virtual int RoleType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[NotMapped]
		public override int Id
		{
			get
			{
				return PersonId;
			}
		}
	}
}
