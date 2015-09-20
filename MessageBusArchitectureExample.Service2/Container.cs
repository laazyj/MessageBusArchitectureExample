using Castle.Windsor;
using Castle.Windsor.Installer;
using NServiceBus;

namespace MessageBusArchitectureExample.Service2
{
    /// <summary>
    /// Configure Castle Windsor as the IoC container in this solution.
    /// </summary>
    public class Container : INeedInitialization
    {
        public static readonly IWindsorContainer Instance = new WindsorContainer();

        public void Customize(BusConfiguration configuration)
        {
            Instance.Install(FromAssembly.This());
        }
    }
}
