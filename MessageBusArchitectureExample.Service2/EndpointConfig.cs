using System;
using System.Linq;
using System.Reflection;
using MessageBusArchitectureExample.Service1.Messages.Events;
using MessageBusArchitectureExample.Service2.Messages.Commands;
using NServiceBus;

namespace MessageBusArchitectureExample.Service2
{
    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
    public class EndpointConfig : IConfigureThisEndpoint
    {
        /// <summary>
        /// Assemblies that contain the Message Handlers for this service.
        /// </summary>
        static readonly Assembly[] HandlerAssemblies = { typeof(EndpointConfig).Assembly };
        /// <summary>
        /// Assemblies containing messages used by this service.
        /// </summary>
        static readonly Assembly[] MessageAssemblies =
        {
            typeof(MyCommand).Assembly, 
            typeof(IFireAndForgetCommandReceivedEvent).Assembly
        };

        public void Customize(BusConfiguration configuration)
        {
            // NServiceBus provides the following durable storage options
            // To use RavenDB, install-package NServiceBus.RavenDB and then use configuration.UsePersistence<RavenDBPersistence>();
            // To use SQLServer, install-package NServiceBus.NHibernate and then use configuration.UsePersistence<NHibernatePersistence>();

            // If you don't need a durable storage you can also use, configuration.UsePersistence<InMemoryPersistence>();
            // more details on persistence can be found here: http://docs.particular.net/nservicebus/persistence-in-nservicebus

            //Also note that you can mix and match storages to fit you specific needs. 
            //http://docs.particular.net/nservicebus/persistence-order

            // For this example we are using InMemoryPersistence only.
            configuration.UsePersistence<InMemoryPersistence>();

            // Log using NLog
            NServiceBus.Logging.LogManager.Use<NLogFactory>();

            // Specify the assemblies to be loaded, rather than scanning all assemblies in the application folder.
            configuration.AssembliesToScan(MessageAssemblies
                .Union(HandlerAssemblies));
            // Use NServiceBus Unobtrusive Mode so that our Messages are just POCOs
            configuration.Conventions()
                .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.EndsWith("Commands"))
                .DefiningEventsAs(t => t.Namespace != null && t.Namespace.EndsWith("Events"))
                .DefiningMessagesAs(t => t.Namespace != null && t.Namespace == "Messages")
                .DefiningDataBusPropertiesAs(p => p.Name.EndsWith("DataBus"))
                .DefiningExpressMessagesAs(t => t.Name.EndsWith("Express"))
                .DefiningTimeToBeReceivedAs(t => t.Name.EndsWith("Expires")
                    ? TimeSpan.FromSeconds(30) : TimeSpan.MaxValue);
            // Use the existing Castle Windsor container
            configuration.UseContainer<WindsorBuilder>(c => c.ExistingContainer(Container.Instance));
            // Use JSON serialisation
            configuration.UseSerialization<JsonSerializer>();
            // Use MSMQ for transport
            configuration.UseTransport<MsmqTransport>();
            // Don't purge existing messages on service startup
            configuration.PurgeOnStartup(false);
        }
    }
}
