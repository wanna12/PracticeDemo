using System;

namespace ASPDOTNET.Delegate
{
    public abstract class AbstractMethodAttribute:Attribute
    {
        public string beforeMsg;
        public string afterMsg;

        public abstract Action doSomething(Action action);
        public abstract void beforeMethod();
        public abstract void afterMethod();
    }
}