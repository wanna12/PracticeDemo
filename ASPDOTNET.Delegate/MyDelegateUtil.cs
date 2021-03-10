using System;
using System.Reflection;

namespace ASPDOTNET.Delegate
{
    public class MyDelegateUtil
    {
        public static void showSomething()
        {
            MyAction myAction=new MyAction();
            Type type = myAction.GetType();
            MethodInfo methodInfo = type.GetMethod("method");

            Action action=new Action(()=>
            {
                methodInfo.Invoke(myAction, null);
            });

            if (methodInfo.IsDefined(typeof(AbstractMethodAttribute),true))
            {
                var list = methodInfo.GetCustomAttributes<AbstractMethodAttribute>();
                foreach (AbstractMethodAttribute attribute in list)
                {
                    attribute.doSomething(action);
                }
            }
            action.Invoke();
        }
    }
}