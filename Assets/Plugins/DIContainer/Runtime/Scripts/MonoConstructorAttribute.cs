using System;

namespace DiContainer
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class MonoConstructorAttribute : Attribute
    {
        
    }
}