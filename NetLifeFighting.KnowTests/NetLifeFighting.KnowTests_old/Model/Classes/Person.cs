using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	/// <summary>
	/// ������������
	/// </summary>
	public class Person
	{
		public Person()
		{
			PersonAnswers = new HashSet<PersonAnswer>();
		}

		/// <summary>
		/// �������������
		/// </summary>
		public virtual int PersonId { get; set; }

		/// <summary>
		/// ���
		/// </summary>
		public virtual string Nickname { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public virtual string Password { get; set; }

		/// <summary>
		/// ������ ������������
		/// </summary>
		public virtual ISet<PersonAnswer> PersonAnswers { get; set; } 
	}
}
