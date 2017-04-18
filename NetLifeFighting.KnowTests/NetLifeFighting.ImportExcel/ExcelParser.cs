using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Spire.Xls;

namespace NetLifeFighting.ImportExcel
{
	/// <summary>
	/// 
	/// </summary>
	public class ExcelParser
	{
		/// <summary>
		/// Соответствие номера столбца заголовку
		/// </summary>
		private readonly Dictionary<string, int> _importTemplate;

		/// <summary>
		/// эксель
		/// </summary>
		private readonly Workbook _workbook;

		/// <summary>
		/// текущий лист
		/// </summary>
		private Worksheet _currentSheet;

		public ExcelParser(string excelFile, Dictionary<string, int> importTemplate)
		{
			_workbook = new Workbook();
			_workbook.LoadFromFile(@excelFile);

			_importTemplate = importTemplate;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		private bool CheckHeadersInRow(CellRange row)
		{
			var cells = row.Cells;

			if (cells.Length != _importTemplate.Count)
			{
				return false;
			}

			foreach (var cell in cells)
			{
				var cellValue = cell.RichText.Text;

				int columnNum;
				if (!_importTemplate.TryGetValue(cellValue, out columnNum))
				{
					return false;
				}

				if (cell.Column != columnNum)
				{
					return false;
				}
			}

			return true;
		}

		private string GetTestTitle()
		{
			return _currentSheet.Name;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public int GetHeadersRow()
		{
			const int noHeadersRow = -1;

			var rows = _currentSheet.Rows;

			var headersRow = rows.TakeWhile(row => !row.Cells[0].HasNumber).FirstOrDefault(CheckHeadersInRow);
			if (headersRow == null)
			{
				return noHeadersRow;
			}
			return headersRow.Row;
		}

		public void SetSheet(int index)
		{
			_currentSheet = _workbook.Worksheets[index];
		}

		public QuestRow[] Parse()
		{
			int currentRow = GetHeadersRow();

			if (currentRow == -1)
			{
				throw new Exception("Документ не соответствует текущему шаблону");
			}

			// заголовок теста
			string testTitle = GetTestTitle();

			// коллекция вопросов
			List<QuestRow> questRows = new List<QuestRow>();

			// чтение строк
			int length = _currentSheet.Rows.Length;

			int i = 0;
			while (i < length)
			{
				QuestRow row;
				i = ReadNext(i, out row);

				if (row == null)
				{
					continue;
				}

				row.TestTitle = testTitle;
				questRows.Add(row);
			}

			return questRows.ToArray();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		private bool HasQuestNum(CellRange row)
		{
			var firstCell = row.Cells[0];

			int result;
			return firstCell.HasNumber || int.TryParse(firstCell.Value, out result);
		}

		/// <summary>
		/// Определяет пустую строку
		/// </summary>
		/// <param name="row">строка</param>
		/// <returns></returns>
		private bool IsEmptyRow(CellRange row)
		{
			return row.Cells.All(c => string.IsNullOrEmpty(c.Value));
		}

		/// <summary>
		/// возвращает индекс строки, с которой нужно начинать поиск
		/// </summary>
		/// <param name="startIndex"></param>
		/// <param name="row"></param>
		/// <returns></returns>
		private int ReadNext(int startIndex, out QuestRow row)
		{
			row = null;
			var rows = _currentSheet.Rows;
			int lastIndex = rows.Last().Row;

			// поиск первой строки вопроса
			var startRow = rows.FirstOrDefault(r => r.Row >= startIndex + 1 && HasQuestNum(r));

			if (startRow == null)
			{
				return lastIndex;
			}

			// чтение строк под вопрос
			int questNum = Convert.ToInt32(startRow.Cells[0].Value2);
			string questTitle = startRow.Cells[1].Value;
			string solutionDescription = startRow.Cells[4].Value;

			row = new QuestRow(questNum, questTitle, solutionDescription);

			List<CellRange> questRows = new List<CellRange>(new[] { startRow });

			int currentIndex = startRow.Row - 1;
			// в следующих строках заполнен только ответ
			var answersRange = rows
				.SkipWhile(r => r.Row <= currentIndex + 1)
				.TakeWhile(r => !IsEmptyRow(r) && !HasQuestNum(r))
				.ToArray();

			questRows.AddRange(answersRange);

			// чтение ответов
			string[] answers = questRows.Select(qr => qr.Cells[3].Value).ToArray();
			row.Answers = answers;

			// чтение картинок
			int topRow = questRows.First().Row;
			int bottomRow = questRows.Last().Row;

			// определяются номера столбцов Картинка и Решение в рамках текущего шаблона
			var importDescription = new ImportDescription();
			int imgColumn = _importTemplate[importDescription.Img];
			int solutionColumn = _importTemplate[importDescription.Solution];

			// изображение может выходить за границы, поэтому коррекция +-1
			row.QuestPictures = ReadPictures(topRow - 1, bottomRow + 1, imgColumn - 1, imgColumn + 1);
			row.SolutionPictures = ReadPictures(topRow - 1, bottomRow + 1, solutionColumn - 1, solutionColumn + 1);

			return questRows.Last().Row;
		}

		/// <summary>
		/// Читает изображения по координатам
		/// </summary>
		/// <param name="topRow">верхняя строка</param>
		/// <param name="bottomRow">нижняя строка</param>
		/// <param name="leftColumn">левая графа</param>
		/// <param name="rightColumn">правая графа</param>
		/// <returns></returns>
		private byte[][] ReadPictures(int topRow, int bottomRow, int leftColumn, int rightColumn)
		{
			// коллекция картинок на листе
			var pictures = _currentSheet.Pictures;
			
			// картинки в рамках указанных координат
			byte[][] convertPictures = pictures.Cast<ExcelPicture>()
				.Where(
					pic =>
						pic.TopRow >= topRow && pic.BottomRow <= bottomRow && pic.LeftColumn >= leftColumn &&
						pic.RightColumn <= rightColumn
				)
				// применить к каждой картинке преобразование
				.Select(pic => ImageToByteArray(pic.Picture))
				.ToArray();

			return convertPictures;
		}

		/// <summary>
		/// конвертирует картинку в массив байтов
		/// </summary>
		/// <param name="image"></param>
		/// <returns></returns>
		private byte[] ImageToByteArray(Image image)
		{
			using (var ms = new MemoryStream())
			{
				image.Save(ms, image.RawFormat);
				return ms.ToArray();
			}
		}
	}
}
