using System;

namespace TeenyDependencyInjector.Exceptions
{
    public class DependencyBindingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyBindingException"/> class.
        /// </summary>
        public DependencyBindingException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyBindingException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DependencyBindingException(string message) : base(message)
        {
        }
    }
}
