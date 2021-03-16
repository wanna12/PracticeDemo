using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1.TaskPratice
{
    public class LogHelper
    {
        /// <summary>
        /// 日志队列
        /// </summary>
        private static Queue<LogModel> queue=new Queue<LogModel>();

        private static LogHelper instance = new LogHelper();
        public static LogHelper CurrentInstance
        {
            get { return instance; }
        }

        public LogHelper()
        {
            start();
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="model"></param>
        public void LogActionInfo(LogModel model)
        {
            queue.Enqueue(model);
        }

        /// <summary>
        /// 启动
        /// </summary>
        private void start()
        {
            Thread thread=new Thread(executeQueue);
            thread.IsBackground = true;//后台线程
            thread.Start();
        }

        /// <summary>
        /// 执行队列
        /// </summary>
        private void executeQueue()
        {
            while (true)
            {
                if (queue.Count>0)
                {
                    //队列有任务
                    try
                    {
                        dequeue();//取出任务执行
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 取出队列中的任务
        /// </summary>
        private void dequeue()
        {
            while (queue.Count>0)
            {
                //队列中有任务，取出
                try
                {
                    LogModel model = queue.Dequeue();
                    writeLogToFile(model);
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// 日志记入文件
        /// </summary>
        /// <param name="model"></param>
        private void writeLogToFile(LogModel model)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
            if (!Directory.Exists(path))
            {
                //文件夹不存在，创建
                Directory.CreateDirectory(path);
            }

            string logPath = Path.Combine(path, $"logActionInfo{DateTime.Today:yyyyMMdd}.log");
            if (!File.Exists(logPath))
            {
                //文件不存在
                File.Create(logPath);
            }
            //写入日志
            string content = JsonConvert.SerializeObject(model);
            using (StreamWriter writer=new StreamWriter(logPath,true))
            {
                writer.WriteLine(content);
            }
        }
    }
}