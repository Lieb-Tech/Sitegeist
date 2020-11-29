using System;

namespace Sitegeist.Scripting.Loggers
{
    public class NullLogger : ILogger
    {
        public void AddMessageType(MessageTypes messageType)
        {
            /// Do nothing here
        }

        public void Log(string message, MessageTypes messageType)
        {
            /// Do nothing here
        }

        public void SetMessageType(MessageTypes messageType)
        {
            /// Do nothing here
        }
    }
}
