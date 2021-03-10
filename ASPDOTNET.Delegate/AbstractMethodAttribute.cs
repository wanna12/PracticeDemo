using System;

namespace ASPDOTNET.Delegate
{
    public abstract class AbstractMethodAttribute:Attribute
    {
        public abstract Action doSomething(Action action);
    }
}