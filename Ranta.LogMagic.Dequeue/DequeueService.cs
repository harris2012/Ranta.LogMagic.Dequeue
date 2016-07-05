using Ranta.LogMagic.Dequeue.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Ranta.LogMagic.Dequeue
{
    public partial class DequeueService : ServiceBase
    {
        MessageProcessor processor = null;

        public DequeueService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                LocalLog.Info("Starting MessageProcessor.");

                processor = new MessageProcessor();

                processor.Start();

                LocalLog.Info("MessageProcessor started.");
            }
            catch (Exception ex)
            {
                LocalLog.Error(ex.ToString());
            }

        }

        protected override void OnStop()
        {
            try
            {
                LocalLog.Info("Stopping MessageProcessor.");

                processor.Stop();

                LocalLog.Info("MessageProcessor stopped.");
            }
            catch (Exception ex)
            {
                LocalLog.Error(ex.ToString());
            }
        }
    }
}
