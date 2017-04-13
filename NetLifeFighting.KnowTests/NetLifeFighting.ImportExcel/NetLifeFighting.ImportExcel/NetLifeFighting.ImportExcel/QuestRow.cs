namespace NetLifeFighting.ImportExcel
{
	public class QuestRow
	{
		public int QuestNum { get; set; }

		public string QuestDescription { get; set; }

		public string AnswerType { get; set; }

		public byte[][] QuestPictures { get; set; }

		public string[] Answers { get; set; }

		public string SolutionDescription { get; set; }

		public byte[][] SolutionPictures { get; set; }
	}
}
