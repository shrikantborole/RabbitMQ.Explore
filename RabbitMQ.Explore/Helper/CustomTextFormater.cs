using System;
using System.Text;

namespace RabbitMQ.Explore.Helper
{
    public class CustomTextFormater
    {
        public static string FormatText(string logMessage)
        {
            StringBuilder str = new StringBuilder();
            try
            {
                str.AppendLine("Log Entry : ");
                str.AppendLine(string.Format("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString()));
                str.AppendLine($"{logMessage}");
                str.AppendLine("-------------------------------");
                return str.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return string.Empty;
        }
    }
}
