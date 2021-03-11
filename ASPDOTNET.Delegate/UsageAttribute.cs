using System;

namespace ASPDOTNET.Delegate
{
    public class UsageAttribute:AbstractMethodAttribute
    {
        public override Action doSomething(Action action)
        {
            return new Action(() => { action.Invoke(); });
        }

        public override void beforeMethod()
        {
            Console.WriteLine(beforeMsg);
        }

        public override void afterMethod()
        {
            Console.WriteLine(afterMsg);
        }

        public UsageAttribute(string beforeMsg, string afterMsg)
        {
            this.beforeMsg = beforeMsg;
            this.afterMsg = afterMsg;
        }
    }
}