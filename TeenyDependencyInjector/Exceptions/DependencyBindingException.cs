using System;

namespace TeenyDependencyInjector.Exceptions
{
    public class DependencyBindingException : Exception
    {
        public DependencyBindingException() { }
        public DependencyBindingException(string message) : base(message)
        {
        }
    }
}
