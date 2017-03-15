using System.Web.Mvc;

namespace NetLifeFighting.KnowTests.Web.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return Redirect("~/App");
		}
	}
}
