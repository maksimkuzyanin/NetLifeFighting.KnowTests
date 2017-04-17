namespace NetLifeFighting.ImportExcel
{
	/// <summary>
	/// описывает строку в ексцель
	/// </summary>
	public class QuestRow
	{
		public QuestRow(string testTitle, int questNum, string questTitle, string solutionDescription)
		{
			TestTitle = testTitle;
			QuestNum = questNum;
			QuestTitle = questTitle;
			SolutionDescription = solutionDescription;
		}

		public QuestRow(int questNum, string questTitle, string solutionDescription)
		{
			QuestNum = questNum;
			QuestTitle = questTitle;
			SolutionDescription = solutionDescription;
		}

		/// <summary>
		/// заголовок теста
		/// </summary>
		public string TestTitle { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int QuestNum { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string QuestTitle { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string AnswerType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public byte[][] QuestPictures { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string[] Answers { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string SolutionDescription { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public byte[][] SolutionPictures { get; set; }
	}
}
