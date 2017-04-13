using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
		private Dictionary<int, string> _importTemplate;

		private ImportType? _importType;

		/// <summary>
		/// Режим импорта
		/// </summary>
		public ImportType ImportType
		{
			get { return _importType.GetValueOrDefault(ImportType.Simple); }
		}

		/// <summary>
		/// эксель
		/// </summary>
		private readonly Workbook _workbook;

		/// <summary>
		/// текущий лист
		/// </summary>
		private Worksheet _currentSheet;

		/// <summary>
		/// текущая строка
		/// </summary>
		public int CurrentRow { get; set; }


		public ExcelParser(string excelFile, [Optional][DefaultParameterValue(ImportType.Simple)] ImportType importType)
		{
			_workbook = new Workbook();
			_workbook.LoadFromFile(@excelFile);

			ImportDescription
		}

		public int GetHeadersRow()
		{
			var rows = _currentSheet.Rows;

			if (rows == null || !rows.Any())
			{
				throw new Exception("Нет строк для импорта");
			}

			if (rows.First().Cells.Length != _importTemplate.Length)
			{
				throw new Exception("Неверный шаблон");
			}

			foreach (var row in rows)
			{
				if (row.Cells.Length != _importTemplate.Length)
				{
					
				}
			}

			Func<CellRange, bool> hasHeaders = (row) =>
			{
				var cells = row.Cells;

				if (cells.Length != _importTemplate.Length)
				{
					return false;
				}

				for (int i = 0; i < cells.Length; i++)
				{
					if (!cells[i].RichText.Text.Equals(_importTemplate[i]))
					{
						return false;
					}
				}

				return true;
			};
		}

		/// <summary>
		/// 
		/// </summary>
		private void Validate()
		{
			
		}

		/*public QuestRow Next()
		{
			
		}*/

		public void SetSheet(int index)
		{
			_currentSheet = _workbook.Worksheets[index];
		}
	}
}
