using DrawingTool.MVC.App_Start;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DrawingTool.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AreaRegistration.RegisterAllAreas();
            AutofacConfig.Configure();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}
