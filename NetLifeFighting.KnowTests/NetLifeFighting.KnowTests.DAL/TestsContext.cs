using System.Data.Entity;
using NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework;

namespace NetLifeFighting.KnowTests.DAL
{
	/// <summary>
	/// Контекст БД
	/// </summary>
	public class TestsContext : DbContext
	{
		public TestsContext(): base("name=TestsContext")
		{
		}

		/// <summary>
		/// Тесты
		/// </summary>
		public DbSet<Test> Tests { get; set; }

		/// <summary>
		/// Вопросы
		/// </summary>
		public DbSet<Question> Questions { get; set; }

		/// <summary>
		/// Тесты - вопросы
		/// </summary>
		public DbSet<TestQuestion> TestsQuestions { get; set; } 

		/// <summary>
		/// Ответы
		/// </summary>
		public DbSet<Answer> Answers { get; set; }

		/// <summary>
		/// Ответы на вопрос
		/// </summary>
		public DbSet<QuestAnswer> QuestsAnswers { get; set; }

		/// <summary>
		/// Пользователи
		/// </summary>
		public DbSet<Person> Persons { get; set; }

		/// <summary>
		/// Пользовательские ответы
		/// </summary>
		public DbSet<PersonAnswer> PersonAnswers { get; set; }

		/// <summary>
		/// Файловые вложения
		/// </summary>
		public DbSet<FileAttachment> FileAttachments { get; set; }

		/// <summary>
		/// Вложения вопроса
		/// </summary>
		public DbSet<QuestAttachment> QuestAttachments { get; set; }

		/// <summary>
		/// Вложения к связке вопрос-ответ
		/// </summary>
		public DbSet<QuestAnswerAttachment> QuestAnswerAttachments { get; set; }

		/*protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			// настройки для маппинга
			modelBuilder.Configurations.Add(new AnswerMapping());
			modelBuilder.Configurations.Add(new PersonAnswerMapping());
			modelBuilder.Configurations.Add(new PersonMapping());
			modelBuilder.Configurations.Add(new QuestionMapping());
			modelBuilder.Configurations.Add(new TestMapping());
			modelBuilder.Configurations.Add(new TestQuestionMapping());

			base.OnModelCreating(modelBuilder);
		}*/
	}
}
