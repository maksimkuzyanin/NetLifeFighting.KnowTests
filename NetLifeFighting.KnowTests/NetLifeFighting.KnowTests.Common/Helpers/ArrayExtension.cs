using System.Collections.Generic;
using System.Linq;

namespace NetLifeFighting.KnowTests.Common.Helpers
{
	public static class ArrayExtension
	{
		public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
		{
			if (collection == null)
			{
				return true;
			}
			return !collection.Any();
		}

		/// <summary>
		/// Преобрзование массива элементов в строковое перечисление разделенное запятой
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="arr"></param>
		/// <param name="wrapStrings"></param>
		/// <returns></returns>
		public static string CommaJoin<T>(this IEnumerable<T> arr, bool wrapStrings)
		{
			return CommaJoin(arr, wrapStrings, ",");
		}

		public static string CommaJoin<T>(this IEnumerable<T> arr, bool wrapStrings, string delimiter)
		{
			string pattern = "{0}";
			if (wrapStrings && typeof(T) == typeof(string))
			{
				pattern = "'{0}'";
			}
			if (arr.IsNullOrEmpty())
			{
				return string.Empty;
			}
			return arr.Aggregate(string.Empty, (seed, x) => seed += delimiter + string.Format(pattern, (object)x)).Substring(delimiter.Length);
		}

		public static string CommaJoin<T>(this IEnumerable<T> arr)
		{
			return CommaJoin(arr, true);
		}
	}
}
