using Ranta.LogMagic.Common;
using Ranta.LogMagic.Contract;
using Ranta.LogMagic.Dequeue.Dal;
using Ranta.LogMagic.Dequeue.Dal.Entity;
using Ranta.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ranta.LogMagic.Dequeue.Core
{
    internal class LogEventManager
    {
        LogEventDal logEventDal = null;

        public LogEventManager()
        {
            logEventDal = new LogEventDal();
        }

        public void ProcessRequest(MessageBase<LogEvent> message)
        {
            try
            {
                var key = message.Guid;

                LogEventEntity entity = ToEntity(message);

                if (entity != null)
                {
                    using (var sqlConn = SqlProvider.GetSqlConnection(AppConst.RantaMaster))
                    {
                        logEventDal.InsertLogEventEntity(entity, sqlConn);
                    }
                }
            }
            catch (Exception ex)
            {
                LocalLog.Error(ex.ToString());
            }
        }

        private LogEventEntity ToEntity(MessageBase<LogEvent> message)
        {
            LogEventEntity entity = null;

            if (message != null)
            {
                entity = new LogEventEntity();

                entity.Guid = message.Guid;

                if (message.Content != null)
                {
                    if (message.Content.Header != null)
                    {
                        entity.AppId = message.Content.Header.AppId;
                        entity.LogTypeId = (int)message.Content.Header.LogEventType;
                        entity.Source = message.Content.Header.Source;
                        entity.CreateTime = message.Content.Header.CreateTime;
                    }
                    if (message.Content.Body != null)
                    {
                        entity.Title = message.Content.Body.Title;
                        entity.Content = message.Content.Body.Detail;
                    }
                }
            }

            return entity;
        }
    }
}
