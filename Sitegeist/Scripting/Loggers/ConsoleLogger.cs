using System;

namespace Sitegeist.Scripting.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public MessageTypes MessageType { get; private set; }

        public void AddMessageType(MessageTypes messageType)
        {
            MessageType = MessageType | messageType;
        }

        public void SetMessageType(MessageTypes messageType)
        {
            MessageType = messageType;
        }
        public void Log(string message, MessageTypes messageType)
        {
            if (MessageType.HasFlag(messageType))
            {
                if (messageType.HasFlag(MessageTypes.ScriptAction))
                    message = $">>>> {message}";
                else if (messageType.HasFlag(MessageTypes.ScriptExpect))
                    message = $"<<<< {message}";

                Console.WriteLine(message);
            }
        }
    }
}
