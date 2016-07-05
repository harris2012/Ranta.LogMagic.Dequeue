using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ranta.LogMagic.Dequeue.Core
{
    public static class LocalLog
    {
        public static void Error(string message)
        {
            EventLog log = new EventLog(AppConst.SourceName);
            log.Source = AppConst.SourceName;

            if (!EventLog.SourceExists(AppConst.SourceName))
            {
                EventLog.CreateEventSource(AppConst.SourceName, AppConst.SourceName);
            }

            log.WriteEntry(message, EventLogEntryType.Error);
        }

        public static void Info(string message)
        {
            EventLog log = new EventLog(AppConst.SourceName);
            log.Source = AppConst.SourceName;

            if (!EventLog.SourceExists(AppConst.SourceName))
            {
                EventLog.CreateEventSource(AppConst.SourceName, AppConst.SourceName);
            }

            log.WriteEntry(message, EventLogEntryType.Information);
        }

        public static void Warn(string message)
        {
            EventLog log = new EventLog(AppConst.SourceName);
            log.Source = AppConst.SourceName;

            if (!EventLog.SourceExists(AppConst.SourceName))
            {
                EventLog.CreateEventSource(AppConst.SourceName, AppConst.SourceName);
            }

            log.WriteEntry(message, EventLogEntryType.Warning);
        }
    }
}
