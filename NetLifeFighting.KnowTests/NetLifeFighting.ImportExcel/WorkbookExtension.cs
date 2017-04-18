using System;
using System.Globalization;
using System.Runtime.InteropServices;
//using NPOI.HSSF.UserModel;
//using NPOI.SS.UserModel;

namespace NetCity.ExportToExcelTest
{
	//public static class WorkbookExtension
	//{
	//	public static ICell GetCell(this ISheet sheet, int rowNum, int cellNum, bool createNew)
	//	{
	//		IRow row = sheet.GetRow(rowNum - 1);
	//		if (row == null)
	//		{
	//			if (!createNew) return null;
	//			row = sheet.CreateRow(rowNum - 1);
	//		}
	//		ICell cell = row.GetCell(cellNum - 1);
	//		if (cell == null)
	//		{
	//			if (!createNew) return null;
	//			cell = row.CreateCell(cellNum - 1);
	//		}
	//		return cell;
	//	}

	//	public static void SetCellValue<T>(this ISheet sheet, int rowNum, int cellNum, T cellValue, [Optional][DefaultParameterValue("0.00")] string format)
	//	{
	//		ICell cell = sheet.GetCell(rowNum, cellNum, true);

	//		if (typeof(T) != typeof(double))
	//		{
	//			//строковый тип
	//			cell.SetCellValue(cellValue as string);
	//		}
	//		else
	//		{
	//			double dValue = Convert.ToDouble(cellValue, new CultureInfo("ru-RU"));
	//			Console.WriteLine("SetCellValue: Устанавливаемое значение - {0}", dValue);
	//			if (dValue % 1 != 0)
	//			{
	//				ICellStyle style = sheet.Workbook.CreateCellStyle();
	//				style.CloneStyleFrom(cell.CellStyle);
	//				style.DataFormat = HSSFDataFormat.GetBuiltinFormat(format);
	//				cell.CellStyle = style;
	//			}
	//			cell.SetCellValue(dValue);
	//		}
	//	}

	//	public static object GetCellValue(this ISheet sheet, int rowNum, int cellNum)
	//	{
	//		ICell cell = sheet.GetCell(rowNum, cellNum, false);

	//		if (cell != null)
	//		{
	//			switch (cell.CellType)
	//			{
	//				case CellType.Boolean:
	//					return cell.BooleanCellValue;
	//				case CellType.Numeric:
	//					return cell.NumericCellValue;
	//				case CellType.String:
	//					return cell.NumericCellValue;
	//			}
	//		}

	//		return null;
	//	}
	//}
}
