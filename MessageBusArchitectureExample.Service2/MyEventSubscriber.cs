using MessageBusArchitectureExample.Service1.Messages.Events;
using NLog;
using NServiceBus;

namespace MessageBusArchitectureExample.Service2
{
    /// <summary>
    /// This Message Handler is subscribed to Service1's IFireAndForgetCommandReceivedEvent
    /// </summary>
    public class MyEventSubscriber :
        IHandleMessages<IFireAndForgetCommandReceivedEvent>
    {
        static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        public void Handle(IFireAndForgetCommandReceivedEvent message)
        {
            Log.Info("A FireAndForgetCommand was processed for Entity '{0}'", message.EntityId);
        }
    }
}
