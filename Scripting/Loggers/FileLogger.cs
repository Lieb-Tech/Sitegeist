using System;
using System.Linq;
using System.IO;
using Sitegeist.Scripting.Engine;
using Sitegeist.Utils;

namespace Sitegeist.Scripting.Loggers
{
    public class FileLogger : ILogger
    {
        /// <summary>
        /// MessageTypes this logger is configured for
        /// </summary>
        public MessageTypes MessageType { get; private set; }
        /// <summary>
        /// File path for the log file
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Setup up the logger
        /// </summary>        
        public FileLogger()
        {
            var dir = ScriptEngine.ConfigurationSettings.Settings.FileLoggerSettings.Folder;
            var file = ScriptEngine.ConfigurationSettings.Settings.FileLoggerSettings.FileName;
            var over = ScriptEngine.ConfigurationSettings.Settings.FileLoggerSettings.Overwrite;

            Path = FilePathBuilder.BuildPath(dir, file, over);
        }

        /// <summary>
        /// Log the message
        /// </summary>
        /// <param name="message">The message to write out</param>
        /// <param name="messageType">What type of message is being logged</param>
        public void Log(string message, MessageTypes messageType)
        {
            if ((messageType & MessageType) == messageType)
            {
                if (MessageType == MessageTypes.ScriptAction)
                    message = $">>>> {message}";
                else if (MessageType == MessageTypes.ScriptExpect)
                    message = $"<<<< {message}";

                File.AppendAllText(Path, $"{DateTime.Now.ToString("HH:mm:ss.fff")}\t{messageType}\t{message}\r\n");
            }
        }

        /// <summary>
        /// Update message types for this logger  
        /// </summary>
        /// <param name="messageType">The type of mssage</param>
        public void AddMessageType(MessageTypes messageType)
        {
            MessageType = MessageType | messageType;
        }
        public void SetMessageType(MessageTypes messageType)
        {
            MessageType = messageType;
        }

    }
}
