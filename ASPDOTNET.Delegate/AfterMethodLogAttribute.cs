using System;

namespace ASPDOTNET.Delegate
{
    public class AfterMethodLogAttribute:AbstractMethodAttribute
    {
        public override Action doSomething(Action action)
        {
            Action actionRes=new Action(() =>
            {
                action.Invoke();
            });
            Console.WriteLine("after method write log");
            return actionRes;
        }
    }
}