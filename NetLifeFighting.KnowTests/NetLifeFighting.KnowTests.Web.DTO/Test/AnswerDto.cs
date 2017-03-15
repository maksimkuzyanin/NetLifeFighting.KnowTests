namespace NetLifeFighting.KnowTests.Web.DTO.Test
{
	/// <summary>
	/// Ответ
	/// </summary>
	public class AnswerDto
	{
		public AnswerDto()
		{
			
		}

		public AnswerDto(int answerId, int questId, string title, string literal, bool isRight, int? priorityNo, int? groupNum)
		{
			AnswerId = answerId;
			QuestId = questId;
			Title = title;
			Literal = literal;
			IsRight = isRight;
			PriorityNo = priorityNo;
			GroupNum = groupNum;
		}

		/// <summary>
		/// Идентификатор
		/// </summary>
		public int AnswerId { get; set; }

		/// <summary>
		/// Идентификатор вопроса
		/// </summary>
		public int QuestId { get; set; }

		/// <summary>
		/// Заголовок
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Литера
		/// </summary>
		public string Literal { get; set; }

		/// <summary>
		/// Верно/Не верно
		/// </summary>
		public bool IsRight { get; set; }

		/// <summary>
		/// Приоритет
		/// </summary>
		public int? PriorityNo { get; set; }

		/// <summary>
		/// Номер группы ответов
		/// </summary>
		public int? GroupNum { get; set; }
	}
}