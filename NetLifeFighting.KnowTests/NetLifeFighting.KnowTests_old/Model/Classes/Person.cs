using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	/// <summary>
	/// Пользователь
	/// </summary>
	public class Person
	{
		public Person()
		{
			PersonAnswers = new HashSet<PersonAnswer>();
		}

		/// <summary>
		/// Идентификатор
		/// </summary>
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
		public virtual ISet<PersonAnswer> PersonAnswers { get; set; } 
	}
}
