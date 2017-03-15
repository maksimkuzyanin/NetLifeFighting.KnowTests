namespace NetLifeFighting.KnowTests.Web.DTO.Person
{
	/// <summary>
	/// 
	/// </summary>
	public class PersonTestResultDto
	{
		public PersonTestResultDto()
		{
			
		}

		public PersonTestResultDto(int rigthQuests, int quests)
		{
			RightQuestsCount = rigthQuests;
			QuestsCount = quests;
		}

		/// <summary>
		/// идентификатор теста
		/// </summary>
		public int TestId { get; set; }

		/// <summary>
		/// название теста
		/// </summary>
		public string TestName { get; set; }

		/// <summary>
		/// количество верных ответов
		/// </summary>
		public int RightQuestsCount { get; set; }

		/// <summary>
		/// количество вопросов в тесте
		/// </summary>
		public int QuestsCount { get; set; }

		/// <summary>
		/// нет ответов на тест
		/// </summary>
		public bool IsEmpty { get; set; }
	}
}
