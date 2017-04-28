using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetLifeFighting.KnowTests.Common.Enums;
using NetLifeFighting.KnowTests.Common.Helpers;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	/// <summary>
	/// ������
	/// </summary>
	[Table("Question")]
	public class Question
	{
		public Question()
		{
			TestQuestions = new HashSet<TestQuestion>();
			QuestsAnswers = new HashSet<QuestAnswer>();
			QuestAttachments = new HashSet<QuestAttachment>();
		}

		/// <summary>
		/// �������������
		/// </summary>
		[Key]
		public virtual int Questid { get; set; }

		/// <summary>
		/// ������������� ������������� ������� (������ ������������)
		/// </summary>
		public virtual int? GroupQuestId { get; set; }

		/// <summary>
		/// ������������
		/// </summary>
		public virtual string Title { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public virtual string Literal { get; set; }

		/// <summary>
		/// ������� ���������
		/// </summary>
		public virtual string LevelOfDifficulty { get; set; }

		/// <summary>
		/// ���
		/// </summary>
		public virtual string AnswerType { get; set; }

		/// <summary>
		/// �����
		/// </summary>
		public virtual ISet<TestQuestion> TestQuestions { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public virtual ISet<QuestAnswer> QuestsAnswers { get; set; }

		/// <summary>
		/// �������� �������
		/// </summary>
		public virtual ISet<QuestAttachment> QuestAttachments { get; set; }
	}
}
