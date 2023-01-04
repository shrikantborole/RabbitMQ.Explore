using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace RabbitMQ.Explore.Logging
{
    public class Logger
    {

        public static void LogWriter(string logMessage)
        {
            LogWrite(logMessage);
        }

        private static void LogWrite(string logMessage)
        {
            var configurationBuilder = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var parentSection = configurationBuilder.Build().GetSection("Logger");
            string path = parentSection.GetSection("FilePath").Value;
            var m_exePath = path == null ? Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) : path;
            
            try
            {
                using (StreamWriter w = File.AppendText(m_exePath + "\\" + "log.txt"))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception)
            {
            }
        }

        private static void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception)
            {
            }
        }
    }
}
