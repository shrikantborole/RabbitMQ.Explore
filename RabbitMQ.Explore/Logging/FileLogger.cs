using Microsoft.Extensions.Options;
using RabbitMQ.Explore.Helper;
using RabbitMQ.Explore.Interface;
using RabbitMQ.Explore.Models;
using System;
using System.IO;
using System.Reflection;

namespace RabbitMQ.Explore.Logging
{
    public class FileLogger : ILogger
    {

        private readonly IOptions<LoggerOptions> _loggerOptions;

        public FileLogger(IOptions<LoggerOptions> loggerOptions)
        {
            _loggerOptions = loggerOptions;
        }


        public void Log(string logMessage)
        {
            LogWrite(logMessage);
        }

        private void LogWrite(string logMessage)
        {
            string path = _loggerOptions.Value.FilePath;
            var m_exePath = path == null ? Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) : path;
            try
            {
                using (StreamWriter txtWriter = File.AppendText(m_exePath + "\\" + "log.txt"))
                {
                    txtWriter.Write(CustomTextFormater.FormatText(logMessage));
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
