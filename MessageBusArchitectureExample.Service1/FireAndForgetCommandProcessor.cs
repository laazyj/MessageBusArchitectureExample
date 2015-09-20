using MessageBusArchitectureExample.Service1.Messages.Commands;
using MessageBusArchitectureExample.Service1.Messages.Events;
using NLog;
using NServiceBus;

namespace MessageBusArchitectureExample.Service1
{
    /// <summary>
    /// Message Handler for FireAndForgetCommands.
    /// For demonstration purposes it just logs that the message was received and publishes an IFireAndForgetCommandReceivedEvent.
    /// </summary>
    public class FireAndForgetCommandProcessor :
        IHandleMessages<FireAndForgetCommand>
    {
        public static ILogger Log = LogManager.GetCurrentClassLogger();

        public IBus Bus { get; set; }

        public void Handle(FireAndForgetCommand message)
        {
            Log.Info("FireAndForgetCommand received for Entity '{0}'", message.EntityId);

            // Do some work here

            // Work is done, let interested parties know.
            Bus.Publish<IFireAndForgetCommandReceivedEvent>(evt =>
            {
                evt.EntityId = message.EntityId;
            });
        }
    }
}
