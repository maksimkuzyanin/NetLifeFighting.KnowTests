using System;
using System.Security.Cryptography;
using System.Text;

namespace NetLifeFighting.KnowTests.Common.Helpers
{
	/// <summary>
	/// Хелпер для шифровки пароля
	/// </summary>
	public class PasswordHelper
	{
		/// <summary>
		/// Для работы с алгоритмами шифрования
		/// </summary>
		private static MD5 _md5;

		/// <summary>
		/// Для работы с алгоритмами шифрования
		/// </summary>
		public static MD5 Md5
		{
			get { return _md5 ?? (_md5 = MD5.Create()); }
		}

		/// <summary>
		/// Вычисляет хэш пароля
		/// </summary>
		/// <param name="password"></param>
		/// <returns></returns>
		static string ComputeHash(string password)
		{
			// взять хэш от байтов строки
			byte[] data = Md5.ComputeHash(Encoding.UTF8.GetBytes(password));

			StringBuilder sBuilder = new StringBuilder();

			// получить шестнадцатеричную строку
			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}

			return sBuilder.ToString();
		}

		/// <summary>
		/// Проверяет введенный пароль
		/// </summary>
		/// <param name="password"></param>
		/// <param name="passwordHash"></param>
		/// <returns></returns>
		public static bool CheckPasswordHash(string password, string passwordHash)
		{
			// получить хэш
			string hashOfInput = ComputeHash(password);

			StringComparer comparer = StringComparer.OrdinalIgnoreCase;

			if (0 == comparer.Compare(hashOfInput, passwordHash))
			{
				return true;
			}
			return false;
		}
	}
}
