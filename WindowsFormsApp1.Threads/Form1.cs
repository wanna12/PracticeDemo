using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Threads
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /**
             * 如何获取funcInt返回值并且保证不卡界面？
             */
//            Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:m:ss"));
//            Func<int> funcInt = ()=>
//            {
//                Thread.Sleep(3000);
//                return DateTime.Today.Year;
//            };
//            Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:m:ss"));
//            Func<int> resFunc = this.ThreadWithReturn<int>(funcInt);
//            Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:m:ss"));
//            int res = resFunc.Invoke();
//            {
//                Console.WriteLine("");
//                Console.WriteLine("这里的执行也需要3秒钟。。。");
//            }
//            Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:m:ss"));
//            Console.WriteLine("res="+res);
            Console.WriteLine(@"******************************");
            ThreadPool.SetMaxThreads(8, 8);
            ManualResetEvent mre = new ManualResetEvent(true);
            for (int i = 0; i < 10; i++)
            {
                int k = i;
                Console.WriteLine(k);
                ThreadPool.QueueUserWorkItem(t =>
                {
                    Console.WriteLine(@"ThreadId=" + Thread.CurrentThread.ManagedThreadId.ToString("00"));
                    if (k == 9)
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

        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            /*
             * 一个老师上课，一节课分四个部分讲
             * 讲完课点同学上来写题目
             * 第一个同学写完奖励5积分
             * 第一个同学写完老师开始检查答案
             * 所有同学写完，老师开始讲评题目
             */
            Console.WriteLine(@"****************Task******************");
            Console.WriteLine(@"老师开始上课");
            Console.WriteLine(@"part01 start");
            Console.WriteLine(@"part01 end");
            Console.WriteLine(@"part02 start");
            Console.WriteLine(@"part02 end");
            Console.WriteLine(@"part03 start");
            Console.WriteLine(@"part03 end");
            Console.WriteLine(@"part04 start");
            Console.WriteLine(@"part04 end");
            Console.WriteLine(@"课程内容已讲完");
            Console.WriteLine(@"点四位同学上来写题目");

            TaskFactory taskFactory=new TaskFactory();
            List<Task> tasks=new List<Task>();
            tasks.Add(taskFactory.StartNew(() => { this.DoExersice("阿珍", "数组"); }));
            tasks.Add(taskFactory.StartNew(() => { this.DoExersice("阿强", "指针"); }));
            tasks.Add(taskFactory.StartNew(() => { this.DoExersice("阿黄", "多线程"); }));
            tasks.Add(taskFactory.StartNew(() => { this.DoExersice("二狗", "委托"); }));
            /*第一种方式，waitany  waitall会阻塞当前线程
             Task.WaitAny(tasks.ToArray());
            Console.WriteLine(@"第一个同学完成了题目，老师开始批改"); ;
            
            Task.WaitAny(tasks.ToArray());
            Console.WriteLine(@"第一个同学完成了题目，奖励5积分");
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine(@"所有题目已完成，老师开始讲评");*/
//            Action<Task> action = new Action<Task>(task => Console.WriteLine(@"第一个同学完成了题目，奖励5积分"));
//            action+=new Action<Task>((task => Console.WriteLine(@"第一个同学完成了题目，老师开始批改")));
//            taskFactory.ContinueWhenAny(tasks.ToArray(), action);
//            Console.WriteLine((object)action);
            //直接用continuewithany
            taskFactory.ContinueWhenAny(tasks.ToArray(), t => Console.WriteLine(@"第一个同学完成了题目，老师开始批改"));
            taskFactory.ContinueWhenAny(tasks.ToArray(), t => Console.WriteLine(@"第一个同学完成了题目，奖励5积分"));
            taskFactory.ContinueWhenAll(tasks.ToArray(), t => Console.WriteLine(@"所有题目已完成，老师开始讲评"));
        }

        private void DoExersice(string name,string work)
        {
            Console.WriteLine($@"{name}同学开始写{work}题目 {DateTime.Now.ToString("yyyy/MM/dd HH:m:ss")}");
            long res = 0;
            for (int i = 0; i < 1_000_000_000; i++)
            {
                res += i;
            }
            Console.WriteLine($@"{name}同学完成{work}题目 {DateTime.Now:yyyy/MM/dd HH:m:ss}");
        }

        /**
         * 发生一个异常如何让全部线程都停止
         */
        private void button3_Click(object sender, EventArgs e)
        {
            {
                //1.设置一个信号量
                bool isError = false;
                try
                {
                    for (int i = 0; i < 50; i++)
                    {
                        string name = $"button3_Click_{i}";
                        if (isError)
                        {
                            //检测到异常
                            throw new Exception($"{name} 发生异常");
                        }
                        else
                        {
                            Console.WriteLine($@"{name} start, ThreadId={Thread.CurrentThread.ManagedThreadId}");
                        }

                        Task.Run((() =>
                        {
                            if ("button3_Click_8".Equals(name) || "button3_Click_12".Equals(name) || "button3_Click_16".Equals(name))
                            {
                                isError = true;
                                throw new Exception($"{name} 异常了……");
                            }
                        }));
                        if (isError)
                        {
                            throw new Exception($"{name} 发生异常");
                        }
                        else
                        {
                            Console.WriteLine($@"{name} end ThreadId={Thread.CurrentThread.ManagedThreadId}");
                        }
                        Console.WriteLine($@"{name} 成功了!!! ThreadId={Thread.CurrentThread.ManagedThreadId}");
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Func<int> func = () =>
            {
                Thread.Sleep(3000);
                Console.WriteLine("异步取值，异步返回，是否卡顿");
                Thread.Sleep(3000);
                return 3000;
            };
            TaskFactory factory=new TaskFactory();
            factory.StartNew((() =>
            {
                Func<int> resFunc = this.ThreadWithReturn(func);
                int res = resFunc.Invoke();
                Console.WriteLine($@"res={res}");
                //return res;
            }));
            Console.WriteLine("主线程完成");
            //
        }

        private Func<T> ThreadWithReturn<T>(Func<T> func)
        {
            T res = default(T);

            ThreadStart threadStart = () => { res = func.Invoke(); };
            Thread thread = new Thread(threadStart);
            thread.Start();
            return new Func<T>((() => {
                thread.Join();
                return res;
            }));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ThreadPool.SetMaxThreads(8, 8);
            ManualResetEvent mre=new ManualResetEvent(false);
            Console.WriteLine(Environment.ProcessorCount);
            for (int i = 0; i < 10; i++)
            {
                int k = i;
                ThreadPool.QueueUserWorkItem((state =>
                {
                    Console.WriteLine($@"threadid={Thread.CurrentThread.ManagedThreadId.ToString("00")} is running");
                    if (k == 9)
                    {
                        mre.Set();
                        mre.Reset();
                    }
                    else
                    {
                        mre.WaitOne();
                    }
                }));
            }

            if (mre.WaitOne())
            {
                Console.WriteLine("all finfished");
            }
        }
    }
}
