using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Xml;

namespace ConsoleApp1.TaskPratice
{
    public class ConfigHelper
    {
        private static string configPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", "taskContent.config");
        private static ConfigHelper instance=new ConfigHelper();
        public static ConfigHelper CurrentInstance
        {
            get { return instance; }
        }
        public ConfigHelper()
        {

        }

        /// <summary>
        /// 从配置中读取人物故事线
        /// </summary>
        /// <returns></returns>
        public List<StoryModel> GetStoryConfig()
        {
            XmlDocument doc=new XmlDocument();
            doc.Load(configPath);
            XmlNodeList nodeList = doc.GetElementsByTagName("Name");
            List<StoryModel> storyList=new List<StoryModel>();
            foreach (XmlNode node in nodeList)
            {
                string name = node.Attributes["value"].Value;
                List<string> events = new List<string>();
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    string eventName = childNode.Attributes["value"].Value;
                    if (!events.Contains(eventName))
                    {
                        events.Add(eventName);
                    }
                }
                storyList.Add(new StoryModel()
                {
                    Name = name,
                    EventList = events
                });
            }

            return storyList;
        }
    }
}