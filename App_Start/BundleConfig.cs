using System.Web.Optimization;

namespace MVC5
{
	public static class eBundle
	{
		public static string bootstrap_Css = "~/Content/css";
		public static string bootstrap_JS = "~/bundles/bootstrap";
		public static string JQuery = "~/bundles/jquery";

		/*
		public static string jqGrid_CSS = "~/bundles/jqGridCss";
		public static string jqGrid_JS = "~/bundles/jqGrid";
		public static string Vue = "~/bundles/VueJS";
		public static string Genesis = "~/bundles/Genesis";
		public static string elUI_JS = "~/Vendor/elUI_JS";
		public static string elUI_CSS = "~/Vendor/elUI_CSS";
		public static string jqDataTables_JS = "~/Vendor/jqDataTablesJs";
		public static string jqDataTables_CSS = "~/Vendor/jqDataTablesCss";

		public static string artTemplate = "~/bundles/artTemplate";
		public static string parsley = "~/bundles/parsley";
		/// <summary>
		/// 備註：這裡不包括載入 elUI 相關套件
		/// </summary>
		public static string Vue_MES = "~/bundles/vue_mes";
		public static string selectize_JS = "~/bundles/selectizeJS";
		public static string selectize_CSS = "~/Vendor/selectizeCSS";
		*/
	}
	public class BundleConfig
	{
		// 如需統合的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle(eBundle.JQuery).Include(
						"~/Scripts/jquery-{version}.js",
						"~/Scripts/jquery.validate*"));

			// 使用開發版本的 Modernizr 進行開發並學習。然後，當您
			// 準備好可進行生產時，請使用 https://modernizr.com 的建置工具，只挑選您需要的測試。
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle(eBundle.bootstrap_JS).Include(
					  "~/Scripts/bootstrap.js"));

			bundles.Add(new StyleBundle(eBundle.bootstrap_Css).Include(
					  "~/Content/bootstrap.css",
					  "~/Content/site.css"));
		}
	}
}
