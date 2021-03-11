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
            actionRes.Invoke();
            Console.WriteLine("after method write log");
            return actionRes;
        }

        public override void beforeMethod()
        {
            //todo
        }

        public override void afterMethod()
        {
            //todo
        }

        public AfterMethodLogAttribute(string beforeMsg, string afterMsg)
        {
            this.beforeMsg = beforeMsg;
            this.afterMsg = afterMsg;
        }
    }
}