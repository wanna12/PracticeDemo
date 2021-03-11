using System;

namespace ASPDOTNET.Delegate
{
    public class MyAction
    {
//        [BeforeMethod]
//        [AfterMethodLog]
        [Usage("before method content","after method content")]
        [BeforeMethod("方法之前","方法之后")]
        public void method()
        {
            Console.WriteLine("this is my action method");
        }
    }
}