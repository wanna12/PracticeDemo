using System;

namespace ASPDOTNET.Delegate
{
    public class BeforeMethodAttribute:AbstractMethodAttribute
    {
        public override Action doSomething(Action action)
        {
            Console.WriteLine("do something before method");
            Action actionRes=new Action(() =>
            {
                action.Invoke();
            });
            return actionRes;
        }
    }
}