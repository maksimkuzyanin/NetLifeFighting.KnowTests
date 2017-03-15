using System;

namespace NetLifeFighting.KnowTests.Web.DTO.DTO
{
	/// <summary>
	/// Тестировние
	/// </summary>
	public class TestDto
	{
		public TestDto()
		{
			
		}

		public TestDto(int testId, string title, int? maxTime, string levelOfDifficulty, string relevanceStatus, DateTime? relevanceDate)
		{
			TestId = testId;
			Title = title;
			MaxTime = maxTime;
			LevelOfDifficulty = levelOfDifficulty;
			RelevanceStatus = relevanceStatus;
			RelevanceDate = relevanceDate;
		}

		/// <summary>
		/// Идентификатор
		/// </summary>
		public int TestId { get; set; }

		/// <summary>
		/// Название
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// максимальное время выполнения
		/// </summary>
		public int? MaxTime { get; set; }

		/// <summary>
		/// Уровень сложности
		/// </summary>
		public string LevelOfDifficulty { get; set; }

		/// <summary>
		/// Статус
		/// </summary>
		public string RelevanceStatus { get; set; }

		/// <summary>
		/// Дата актуальности
		/// </summary>
		public DateTime? RelevanceDate { get; set; }

		#region equality members

		protected bool Equals(TestDto other)
		{
			return TestId == other.TestId && string.Equals(Title, other.Title) && MaxTime == other.MaxTime && string.Equals(LevelOfDifficulty, other.LevelOfDifficulty) && string.Equals(RelevanceStatus, other.RelevanceStatus) && RelevanceDate.Equals(other.RelevanceDate);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((TestDto) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = TestId;
				hashCode = (hashCode*397) ^ (Title != null ? Title.GetHashCode() : 0);
				hashCode = (hashCode*397) ^ MaxTime.GetHashCode();
				hashCode = (hashCode*397) ^ (LevelOfDifficulty != null ? LevelOfDifficulty.GetHashCode() : 0);
				hashCode = (hashCode*397) ^ (RelevanceStatus != null ? RelevanceStatus.GetHashCode() : 0);
				hashCode = (hashCode*397) ^ RelevanceDate.GetHashCode();
				return hashCode;
			}
		}

		#endregion
	}
}