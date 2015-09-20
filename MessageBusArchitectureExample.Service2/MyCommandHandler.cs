using System;
using MessageBusArchitectureExample.Service2.Messages.Commands;
using NLog;
using NServiceBus;

namespace MessageBusArchitectureExample.Service2
{
    public class MyCommandHandler :
        IHandleMessages<MyCommand>
    {
        static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        public void Handle(MyCommand message)
        {
            Log.Info("Executing 'MyCommand' at '{0}'", DateTime.UtcNow);    
        }
    }
}
