using System.Collections.Generic;
using System.Linq;

namespace NetLifeFighting.ImportExcel
{
	/// <summary>
	/// Импорт из ексель в бд
	/// </summary>
	public class ExcelImporter
	{
		/// <summary>
		/// путь к файлу
		/// </summary>
		private string _excelFile;

		/// <summary>
		/// Режим импорта
		/// </summary>
		private readonly ImportType _importType;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="excelFile"></param>
		/// <param name="importType">тип иморта - расширенный, краткий</param>
		public ExcelImporter(string excelFile, ImportType importType = ImportType.Simple)
		{
			_excelFile = excelFile;
			_importType = importType;
		}

		public void Execute()
		{
			var importTemplate = GetImportTemplate();

			ExcelParser parser = new ExcelParser(_excelFile, importTemplate);
			parser.SetSheet(0);
			var rows = parser.Parse();
		}

		/// <summary>
		/// получает шаблон для импорта
		/// </summary>
		/// <returns></returns>
		private Dictionary<string, int> GetImportTemplate()
		{
			// объект с описанием полей ексцеля
			var importDescription = new ImportDescription();

			// привязка полей к номерам столбцов
			string[] importMappings = importDescription.GetOptionalImportMappings(_importType).ToArray();

			// шаблон
			var importTemplate = importMappings
				.Select((s, i) => new { ColumnName = s, CulumnNum = i + 1 })
				.ToDictionary(x => x.ColumnName, y => y.CulumnNum);

			return importTemplate;
		} 
	}
}
