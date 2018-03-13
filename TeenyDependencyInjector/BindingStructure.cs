using System;

namespace TeenyDependencyInjector
{
    /// <summary>
    /// The structure for registering a binding
    /// </summary>
    internal struct BindingStructure
    {
        public readonly Type BindingType;
        public readonly Type ConcreteType;
        public readonly object BindingObject;
        public readonly string BindingName;
        public readonly object[] Parameters;

        public BindingStructure(Type bindingType, Type concreteType, object bindingObject = null, string bindingName = null, params object[] parameters)
        {
            BindingObject = bindingObject;
            BindingType = bindingType;
            BindingName = bindingName;
            Parameters = parameters;
            ConcreteType = concreteType;
        }
    }
}