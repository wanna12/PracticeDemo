using System;

namespace ASPDOTNET.Delegate
{
    public class MyAction
    {
        [BeforeMethod]
        [AfterMethodLog]
        public void method()
        {
            Console.WriteLine("this is my action method");
        }
    }
}