using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Threads
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.SetMaxThreads(8, 8);
            ManualResetEvent mre=new ManualResetEvent(true);
            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(t =>
                {
                    Console.WriteLine("ThreadId=" + Thread.CurrentThread.ManagedThreadId.ToString("00"));
                    if (i == 9)
                    {
                        mre.Set();
                    }
                    else
                    {
                        mre.WaitOne();
                    }
                });

            }

            if (mre.WaitOne())
            {
                Console.WriteLine(@"所有线程执行完成");
            }

            Console.Read();
        }
    }
}
