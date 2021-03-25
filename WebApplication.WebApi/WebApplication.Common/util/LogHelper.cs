using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json;

namespace WebApplication.Common.util
{
    public class LogHelper
    {
        private static LogHelper instance=new LogHelper();
        public static LogHelper Current
        {
            get { return instance; }
        }

        private Queue<LogEnt> logQueue = new Queue<LogEnt>();
        private static readonly object obj = new object();

        private static readonly ILog logInfo = LogManager.GetLogger("loginfo");
        private static readonly ILog logerror = LogManager.GetLogger("logerror");

        private LogHelper()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "config", "logConfig.xml");
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(path));
            Task.Run(LogHandle);
        }

        public void WriteInfoLog(LogEnt ent)
        {
            lock (obj)
            {
                logQueue.Enqueue(ent);
            }
//            if (logInfo.IsInfoEnabled)
        }

        public void WriteErrorLog(string msg, Exception ex)
        {
            logerror.Error(msg,ex);
        }

        private void LogHandle()
        {
            //可能这里不需要做队列处理？log4支持可以设置线程安全？
            LogEnt logEnt = null;
            try
            {
                while (true)
                {
                    while (logQueue.Count > 0)
                    {
                        lock (obj)
                        {
                            logEnt = logQueue.Dequeue();
                        }
                        logInfo.Info(JsonConvert.SerializeObject(logEnt));
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }

    public class LogEnt
    {
        public string func { get; set; }
        public string in_param { get; set; }
        public string out_param { get; set; }
        public long spent_time { get; set; }
        public string user_id { get; set; }

        public LogEnt(string func,string user_id, string in_param, string out_param,long spentTime)
        {
            this.func = func;
            this.in_param = in_param;
            this.out_param = out_param;
            this.spent_time = spentTime;
        }
        public LogEnt() { }
    }
}
