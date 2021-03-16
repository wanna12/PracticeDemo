using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1.TaskPratice
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch=new Stopwatch();
            stopwatch.Start();

            List<Task> taskList=new List<Task>();
            //获取人物线配置内容
            List<StoryModel> storyList = ConfigHelper.CurrentInstance.getStoryConfig();
            

            stopwatch.Stop();
            Console.WriteLine($"天龙八部的故事耗时{stopwatch.ElapsedMilliseconds}ms");

            Console.ReadKey();
        }
    }
}
