using System;

namespace MessageBusArchitectureExample.Service1.Messages.Commands
{
    /// <summary>
    /// An example 'Fire and Forget' Command. 
    /// This is used to Command the system to perform an action, the Sender of the Command
    /// expects that it will eventually be successful. Failures are handled as exceptions.
    /// </summary>
    public class FireAndForgetCommand
    {
        public Guid EntityId { get; set; }
        public DateTime SomeSortOfTimeStampUtc { get; set; }
        public string SomeProperty { get; set; }
        public int AnotherProperty { get; set; }
    }
}
