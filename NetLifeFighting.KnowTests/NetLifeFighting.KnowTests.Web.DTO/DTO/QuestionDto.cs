namespace NetLifeFighting.KnowTests.Web.DTO.DTO
{
	/// <summary>
	/// Вопрос
	/// </summary>
	public class QuestionDto
	{
		public QuestionDto()
		{
			
		}

		public QuestionDto(int questId, string title, string literal, string levelOfDifficulty, string answerType)
		{
			Questid = questId;
			Title = title;
			Literal = literal;
			LevelOfDifficulty = levelOfDifficulty;
			AnswerType = answerType;
		}

		/// <summary>
		/// Идентификатор
		/// </summary>
		public int Questid { get; set; }

		/// <summary>
		/// Идентификатор теста
		/// </summary>
		public int TestId { get; set; }

		/// <summary>
		/// Главный вопрос группы
		/// </summary>
		public int? GroupQuestId { get; set; }

		/// <summary>
		/// Заголовок
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Литера
		/// </summary>
		public string Literal { get; set; }

		/// <summary>
		/// Уровень сложности
		/// </summary>
		public string LevelOfDifficulty { get; set; }

		/// <summary>
		/// Тип ответа
		/// </summary>
		public string AnswerType { get; set; }

		/// <summary>
		/// номер вопроса в тесте
		/// </summary>
		public int QuestNum { get; set; }

		/// <summary>
		/// Максимальное время на вопрос
		/// </summary>
		public int? MaxTime { get; set; }
	}
}