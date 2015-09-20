using System;
using System.Linq;
using Castle.Windsor;
using NServiceBus;

namespace MessageBusArchitectureExample.Web
{
    /// <summary>
    /// Configure the NServiceBus for the MVC application.
    /// </summary>
    public class BusConfig
    {
        public static void ConfigureBus(IWindsorContainer container)
        {
            // Use NLog for logging
            NServiceBus.Logging.LogManager.Use<NLogFactory>();

            var configuration = new BusConfiguration();
            // Only scan known Messages assemblies rather than all assemblies in 'bin' - faster startup time.
            configuration.AssembliesToScan(AppDomain.CurrentDomain.GetAssemblies().Where(a => a.GetName().Name.EndsWith(".Messages")));
            // Use NServiceBus Unobtrusive Mode
            configuration.Conventions()
                .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.EndsWith("Commands"))
                .DefiningEventsAs(t => t.Namespace != null && t.Namespace.EndsWith("Events"))
                .DefiningMessagesAs(t => t.Namespace != null && t.Namespace == "Messages")
                .DefiningDataBusPropertiesAs(p => p.Name.EndsWith("DataBus"))
                .DefiningExpressMessagesAs(t => t.Name.EndsWith("Express"))
                .DefiningTimeToBeReceivedAs(t => t.Name.EndsWith("Expires") 
                    ? TimeSpan.FromSeconds(30) : TimeSpan.MaxValue); 
            // Use existing Castle Windsor container
            configuration.UseContainer<WindsorBuilder>(c => c.ExistingContainer(container));
            // Use JSON for serialisation
            configuration.UseSerialization<JsonSerializer>();
            // Use MSMQ for transport
            configuration.UseTransport<MsmqTransport>();
            // Use InMemory persistence only
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.EnableInstallers();

            // Start the bus
            var bus = Bus.Create(configuration);
            bus.Start();
        }
    }
}