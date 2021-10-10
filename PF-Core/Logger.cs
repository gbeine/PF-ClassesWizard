using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using UnityModManagerNet;

namespace PF_Core
{
    public class Logger
    {
        public static readonly Logger INSTANCE = new Logger();
        
        private UnityModManager.ModEntry.ModLogger _logger;
        private StreamWriter _logfile;

        private Logger()
        {
            String m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _logfile = File.AppendText(m_exePath + "/" + "log.txt");
        }
        
        public void init(UnityModManager.ModEntry modEntry)
        {
            _logger = modEntry.Logger;
        }

        public void Critical(object obj) => Critical(obj?.ToString() ?? "null");
        public void Critical(string message)
        {
            _logger?.Critical(message);
            append(message);
        }

        public void Error(object obj) => Error(obj?.ToString() ?? "null");
        public void Error(string message)
        {
            _logger?.Error(message);
            append(message);
        }

        public void Log(object obj) => Log(obj?.ToString() ?? "null");
        public void Log(string message)
        {
            _logger?.Log(message);
            append(message);
        }

        public void Warning(object obj) => Warning(obj?.ToString() ?? "null");
        public void Warning(string message)
        {
            _logger?.Warning(message);
            append(message);
        }

        [Conditional("DEBUG")]
        public void Debug(object obj) => Debug(obj?.ToString() ?? "null");
        
        [Conditional("DEBUG")]
        public void Debug(string message)
        {
            _logger?.Log(message);
            append(message);
        }
        
        private void append(string logMessage)
        {
            _logfile.WriteLine("{0} {1}: {2}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString(), logMessage);
            _logfile.Flush();
        }
    }
}
