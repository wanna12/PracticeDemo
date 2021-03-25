using System;

namespace ConsoleApp1.TaskPratice
{
    public class LogModel
    {
        /// <summary>
        /// 0-成功 -1失败
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 失败时的失败枚举值
        /// </summary>
        public int errCode { get; set; }

        public string msg;

        public LogModel() { }

        public LogModel(string msg, int status, int errCode)
        {
            this.msg = msg;
            this.status = status;
            this.errCode = errCode;
        }
    }
}