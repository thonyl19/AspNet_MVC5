using System.Web.Mvc;
using System.Web.Optimization;

namespace Bundle
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
	}
}
