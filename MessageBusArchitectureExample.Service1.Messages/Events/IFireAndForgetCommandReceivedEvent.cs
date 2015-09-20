using System;

namespace MessageBusArchitectureExample.Service1.Messages.Events
{
    /// <summary>
    /// Event that is Published AFTER a FireAndForgetCommand is processed.
    /// </summary>
    public interface IFireAndForgetCommandReceivedEvent
    {
        Guid EntityId { get; set; }
    }
}
