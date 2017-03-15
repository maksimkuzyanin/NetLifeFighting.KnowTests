using System;

namespace NetLifeFighting.KnowTests.Common.Helpers
{
	/// <summary>
	/// 
	/// </summary>
	public static class StringExtension
	{
		/// <summary>
		/// Конвертирует строку в значение перечисления
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="s"></param>
		/// <returns></returns>
		public static T ToEnum<T>(this string s) where T : struct
		{
			string tName = typeof(T).Name;

			if (string.IsNullOrEmpty(s))
			{
				throw new ArgumentException(string.Format("Невозможно привести нулевой или пустой аргумент к перечислимому типу {0}", tName));
			}
			if (s.Length > 1)
			{
				throw new ArgumentException(string.Format("Невозможно привести аргумент длиной превышающей один символ к перечислимому типу {0}", tName));
			}

			return (T) Enum.ToObject(typeof(T), s[0]);
		}
	}
}
