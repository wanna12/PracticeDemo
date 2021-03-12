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

        private static readonly ILog logInfo = LogManager.GetLogger("loginfo");
        private static readonly ILog logerror = LogManager.GetLogger("logerror");

        private LogHelper()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "config", "logConfig.xml");
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(path));
        }

        public void WriteInfoLog(LogEnt ent)
        {
//            if (logInfo.IsInfoEnabled)
            {
                logInfo.Info(JsonConvert.SerializeObject(ent));
            }
        }

        public void WriteErrorLog(LogEnt ent, Exception ex)
        {
            if (logerror.IsErrorEnabled)
            {
                logerror.Error(JsonConvert.SerializeObject(ent),ex);
            }
        }
    }

    public class LogEnt
    {
        public string func { get; set; }
        public string in_param { get; set; }
        public string out_param { get; set; }
        public long spent_time { get; set; }

        public LogEnt(string func, string in_param, string out_param,long spentTime)
        {
            this.func = func;
            this.in_param = in_param;
            this.out_param = out_param;
            this.spent_time = spentTime;
        }
    }
}
