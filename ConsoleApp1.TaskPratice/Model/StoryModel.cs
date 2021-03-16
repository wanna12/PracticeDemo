using System.Collections.Generic;

namespace ConsoleApp1.TaskPratice
{
    public class StoryModel
    {
        /// <summary>
        /// 故事角色名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 人物线的事件顺序列表
        /// </summary>
        public List<string> EventList=new List<string>();
    }
}