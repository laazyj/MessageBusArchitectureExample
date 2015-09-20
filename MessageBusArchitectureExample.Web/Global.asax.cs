using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MessageBusArchitectureExample.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var container = ContainerConfig.BootstrapContainer();
            BusConfig.ConfigureBus(container);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_End()
        {
            ContainerConfig.CleanUp();
        }
    }
}
