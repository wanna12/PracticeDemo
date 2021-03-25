using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication.ServiceBll.Model;
using WebApplication.WebApi;
using WebApplication.WebApi.Controllers;

namespace WebApplication.WebApi.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        ValuesController controller = new ValuesController();


        [TestMethod]
        public void Get()
        {
            // 排列
            ValuesController controller = new ValuesController();

            // 操作
            IEnumerable<string> result = controller.Get();

            // 断言
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void GetById()
        {
            // 排列
            ValuesController controller = new ValuesController();

            // 操作
            string result = controller.Get(5);

            // 断言
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // 排列
            ValuesController controller = new ValuesController();

            // 操作
            controller.Post("value");

            // 断言
        }

        [TestMethod]
        public void Put()
        {
            // 排列
            ValuesController controller = new ValuesController();

            // 操作
            controller.Put(5, "value");

            // 断言
        }

        [TestMethod]
        public void Delete()
        {
            // 排列
            ValuesController controller = new ValuesController();

            // 操作
            controller.Delete(5);

            // 断言
        }

        [TestMethod]
        public void TestMethod()
        {
            for(int i = 0; i < 100; i++)
            {
                int k = i;
                Task.Run(() => {
                    TestMethodRequest request = new TestMethodRequest() { 
                        user_id = k.ToString(), 
                        c_ip = "127.0.0.1", 
                        funcName= "testMethod",
                        o_time=$"{DateTime.Now:yyyy/MM/dd HH:mm:ss}"
                    };
                    if (k % 3 == 0)
                    {
                        request.inputStr = "str";
                    }
                    else
                    {
                        request.inputStr = k.ToString();
                    }

                    controller.testMethod(request);
                });
            }
        }
    }
}
