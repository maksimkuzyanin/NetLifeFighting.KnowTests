/*using System;
using System.Globalization;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;*/


using Spire.Xls;


namespace NetLifeFighting.ImportExcel
{
	class Program
	{

		static void Main(string[] args)
		{
			Workbook workbook = new Workbook();
			workbook.LoadFromFile(@"D:\Projects\NetLifeFighting.ImportExcel\NetLifeFighting.ImportExcel\bin\Debug\Test.xlsx");
			Worksheet sheet = workbook.Worksheets[0];
			bool hasPictures = sheet.Rows[8].Cells[2].HasPictures;

			ExcelPicture pic = sheet.Pictures[1];
			var col = pic.RightColumn;
			var row = pic.TopRow;
			

			string excelPath = @"D:\Projects\NetLifeFighting.ImportExcel\NetLifeFighting.ImportExcel\bin\Debug\Test.xlsx";

			ExcelImporter importer = new ExcelImporter(excelPath);
			importer.Execute();
		}
	}
}
