using System.Web.Mvc;

namespace MVC5.Controllers
{
	public class BaseController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		// GET: Base
		public ActionResult test(string name)
		{
			return View(name);
		}
	}
}
