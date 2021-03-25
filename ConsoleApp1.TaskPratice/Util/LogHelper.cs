using System;
using System.Collections.Concurrent;
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
        private static ConcurrentQueue<LogModel> queue = new ConcurrentQueue<LogModel>();

        //锁
        //private static readonly object obj = new object();
        private static AutoResetEvent autoResetEvent=new AutoResetEvent(false);

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
            lock (autoResetEvent)
            {
                queue.Enqueue(model);
                autoResetEvent.Set();
            }
        }


        /// <summary>
        /// 启动
        /// </summary>
        private void start()
        {
            Thread thread = new Thread(executeQueue);
            thread.IsBackground = true;//后台线程
            thread.Start();
        }

        /// <summary>
        /// 执行队列
        /// </summary>
        private void executeQueue()
        {
            LogModel model = null;
            while (true)
            {
                //队列中有任务，取出
                try
                {
                    autoResetEvent.WaitOne();
                    while (queue.Count > 0)
                    {
                        
                        lock (autoResetEvent)
                        {
                            queue.TryDequeue(out model);
                            writeLogToFile(model);
                        }
                        
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}");
                    Console.WriteLine(e.StackTrace);
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

            lock (autoResetEvent)
            {
                //写入日志
                string content = $"{DateTime.Now:yyyy/MM/dd HH:m:ss} - {JsonConvert.SerializeObject(model)}";
                using (StreamWriter writer = new StreamWriter(logPath, true))
                {
                    writer.WriteLine(content);
                }
            }
        }
    }
}