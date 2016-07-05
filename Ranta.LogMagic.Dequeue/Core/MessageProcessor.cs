using Ranta.LogMagic.Common;
using Ranta.LogMagic.Common.Generic;
using Ranta.LogMagic.Contract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ranta.LogMagic.Dequeue.Core
{
    public class MessageProcessor : IDisposable
    {
        private Task workingTask = null;
        private bool stopProcess = false;
        private Msmq<MessageBase<LogEvent>> msmq = null;
        private object lockObject = null;

        private LogEventManager logEventManager = null;

        public MessageProcessor()
        {
            msmq = new Msmq<MessageBase<LogEvent>>(ConfigurationManager.AppSettings["RantaLogQueue"]);

            logEventManager = new LogEventManager();

            lockObject = new object();
        }

        public void Start()
        {
            workingTask = new Task(() =>
            {
                ProcessRequest();
            });

            workingTask.Start();
        }

        public void Stop()
        {
            this.stopProcess = true;

            workingTask.Wait();
        }

        private void ProcessRequest()
        {
            MessageBase<LogEvent> message = null;

            while (!this.stopProcess)
            {
                //LocalLog.Info("Enter while.");

                message = msmq.Dequeue();

                if (message != null)
                {
                    //LocalLog.Info("Begin Process");

                    logEventManager.ProcessRequest(message);
                }

                //LocalLog.Info("Exit while.");
            }
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ((IDisposable)workingTask).Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
