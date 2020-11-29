using System;

namespace Sitegeist.Scripting.Loggers
{
    public interface ILogger
    {
        void Log(string message, MessageTypes messageType);
        void AddMessageType(MessageTypes messageType);

        void SetMessageType(MessageTypes messageType);
    }

    [Flags]
    public enum MessageTypes
    {
        General = 1,
        Internal = 2,
        UserMessage = 4,    
        ScriptAction = 8,
        ScriptExpect = 16,
    }        
}
