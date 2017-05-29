using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	/// <summary>
	/// ������������
	/// </summary>
	[Table("Person")]
	public class Person : Entity
	{
		public Person()
		{
			//PersonAnswers = new HashSet<PersonAnswer>();
		}

		/// <summary>
		/// �������������
		/// </summary>
		[Key]
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
