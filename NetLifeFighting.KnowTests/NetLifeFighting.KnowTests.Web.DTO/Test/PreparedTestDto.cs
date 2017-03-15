using System.Linq;
using NetLifeFighting.KnowTests.Web.DTO.Person;

namespace NetLifeFighting.KnowTests.Web.DTO.Test
{
	/// <summary>
	/// Подготовленное тестирование
	/// </summary>
	public class PreparedTestDto
	{
		/// <summary>
		/// Информация о тесте
		/// </summary>
		public TestDto Test { get; set; }

		/// <summary>
		/// Подготовленные вопросы
		/// </summary>
		public PreparedQuestionDto[] Questions { get; set; }

		/// <summary>
		/// Количество вопросов
		/// </summary>
		public int QuestsCount
		{
			get { return Questions == null || !Questions.Any() ? 0 : Questions.Length; }
		}

		/// <summary>
		/// 
		/// </summary>
		public bool IsEmpty { get; set; }

		/// <summary>
		/// результат пользователя в тесте
		/// </summary>
		public PersonTestResultDto PersonTestResult { get; set; }
	}
}
