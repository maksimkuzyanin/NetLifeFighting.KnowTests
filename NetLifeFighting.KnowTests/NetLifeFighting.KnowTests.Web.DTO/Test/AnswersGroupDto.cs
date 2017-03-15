﻿namespace NetLifeFighting.KnowTests.Web.DTO.Test
{
	/// <summary>
	/// Группа ответов
	/// </summary>
	public class AnswersGroupDto
	{
		/// <summary>
		/// Номер группы
		/// </summary>
		public int? GroupNum { get; set; }

		/// <summary>
		/// Ответы
		/// </summary>
		public AnswerDto[] Answers { get; set; }
	}
}
