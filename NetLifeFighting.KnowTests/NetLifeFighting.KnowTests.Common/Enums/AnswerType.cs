namespace NetLifeFighting.KnowTests.Common.Enums
{
	/// <summary>
	/// Тип ответа на вопрос
	/// </summary>
	public enum AnswerType
	{
		/// <summary>
		/// Один ответ
		/// </summary>
		Single = 'S',

		/// <summary>
		/// Несколько ответов
		/// </summary>
		Multichoice = 'M',

		/// <summary>
		/// Несколько ответов с приоритетом
		/// </summary>
		Priority = 'P',

		/// <summary>
		/// Соответствие
		/// </summary>
		Conformity = 'C'
	}
}
