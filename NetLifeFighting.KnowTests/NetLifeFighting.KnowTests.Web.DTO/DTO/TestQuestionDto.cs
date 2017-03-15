using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLifeFighting.KnowTests.Web.DTO.DTO
{
	/// <summary>
	/// 
	/// </summary>
	public class TestQuestionDto
	{
		public TestQuestionDto()
		{
			Test = new TestInfoDto();
			Question = new QuestionDto();
		}

		/// <summary>
		/// тест
		/// </summary>
		public TestInfoDto Test { get; set; }

		/// <summary>
		/// вопрос
		/// </summary>
		public QuestionDto Question { get; set; }

		/// <summary>
		/// Номер вопроса
		/// </summary>
		public int QuestNum { get; set; }

		/// <summary>
		/// Максимальное время на вопрос
		/// </summary>
		public int? MaxTime { get; set; }
	}
}
