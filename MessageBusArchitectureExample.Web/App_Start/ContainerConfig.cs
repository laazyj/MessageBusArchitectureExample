using System.Web.Mvc;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Castle.Windsor.Mvc;

namespace MessageBusArchitectureExample.Web
{
    /// <summary>
    /// Configure Castle Windsor as the IoC container in this solution.
    /// </summary>
    public class ContainerConfig
    {
        static IWindsorContainer _container;

        public static IWindsorContainer BootstrapContainer()
        {
            _container = new WindsorContainer()
                .Install(FromAssembly.This());
            var controllerFactory = new WindsorControllerFactory(_container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
            return _container;
        }

        public static void CleanUp()
        {
            if (_container == null) return;

            _container.Dispose();
            _container = null;
        }
    }
}