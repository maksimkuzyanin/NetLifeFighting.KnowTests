using System.Web;
using System.Web.Http;
using NetLifeFighting.KnowTests.Common;
using NetLifeFighting.KnowTests.Common.Abstraction.Result;
using NetLifeFighting.KnowTests.Common.Enums;
using NetLifeFighting.KnowTests.Components;
using NetLifeFighting.KnowTests.Web.DTO.Person;
using NetLifeFighting.KnowTests.Web.Helpers;

namespace NetLifeFighting.KnowTests.Web.Controllers
{
	/// <summary>
	/// Для работы с админкой
	/// </summary>
	[RoutePrefix("app/api/admin")] // todo: разобраться с построением пути
    public class AdminController : ApiController
    {
		/// <summary>
		/// Имя администратора
		/// </summary>
	    const string AdminName = "admin";

		/// <summary>
		/// компонента аутентификации
		/// </summary>
		private readonly AuthComponent _authComponent;

		/// <summary>
		/// компонента работы с тестами
		/// </summary>
		private readonly TestComponent _testComponent;

	    public AdminController()
	    {
		    _authComponent = new AuthComponent();
			_testComponent = new TestComponent();
	    }

		[HttpPost]
		[Route("login")]
	    public Result<PersonDto> AdminLogin([FromBody] PersonCredentials credentials)
		{
			// инициализация полномочий
			credentials.Nickname = AdminName;
			credentials.Role = RoleType.Admin;

			// данные администратора
			var adminData = _authComponent.GetPersonData(credentials);

			if (adminData == null)
			{
				return new Result<PersonDto>(ResultStatus.Failure, Messages.kEntryErr);
			}
			return new Result<PersonDto>(adminData);
		}

		[HttpPost]
		[Route("import")]
		public void ImportTests()
		{
			var fileBytes = GetUploadFileBytes();
			// пока импорт неуниверсальный
			_testComponent.ImportTests(fileBytes);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private byte[] GetUploadFileBytes()
		{
			// загруженный файл
			var file = HttpContext.Current.Request.Files[0];
			// файловый поток
			var stream = file.InputStream;
			// длина содержимого файла
			var contentLength = file.ContentLength;
			// инициализация массива байтов
			byte[] fileBytes = new byte[file.ContentLength];
			// чтение байтов потока
			stream.Read(fileBytes, 0, contentLength);

			return fileBytes;
		}
	}
}
