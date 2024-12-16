using System;
using System.Configuration;
using System.Diagnostics;
using System.Text;

namespace Hotel.Grobal
{
    public class clsLogger
    {
        public static void LogError(string errorType, Exception ex)
        {
            string sourceName = ConfigurationManager.AppSettings["ProjectName"];

            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, "Application");
            }
            string errorMessage = $"{errorType} in: {ex.Source}\n" +
                $"Exception Message: {ex.Message}\n" +
                $"Exception Type: {ex.GetType().Name}\n" +
                $"Stack Trace: {ex.StackTrace}\n" +
                $"Error Location: {ex.TargetSite.Name}\n";

            EventLog.WriteEntry(sourceName, errorMessage, EventLogEntryType.Error);
        }


    }
}
