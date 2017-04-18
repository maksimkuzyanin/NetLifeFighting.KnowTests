using System.Web.Http;
using NetLifeFighting.KnowTests.DAL.EntityFramework.Persons;

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
		/// пользовательский репозиторий
		/// </summary>
	    private PersonDao _personDao;

	    public AdminController()
	    {
		    _personDao = new PersonDao();
	    }

		[HttpPost]
		[Route("auth")]
	    public void AdminLogin(string password)
	    {
			//var admin = 
	    }
    }
}
