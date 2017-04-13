using System.Collections.Generic;

namespace NetLifeFighting.ImportExcel
{
	public class ImportDescription
	{
		public string Num = "№" ;
		public string Quest = "Вопрос";
		public string AnswerType = "Тип";
		public string Img = "Картинка";
		public string Answers = "Ответы";
		public string Solution = "Решение";

		public IEnumerable<string>GetCommonRequiredMappings(ImportType importType)
		{
			yield return Num;
			yield return Quest;

			if (importType == ImportType.Composite)
			{
				yield return AnswerType;
			}

			yield return Img;
			yield return Answers;
			yield return Solution;
		}
	}
}
