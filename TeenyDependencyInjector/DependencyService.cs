using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using TeenyDependencyInjector.Exceptions;
using TeenyDependencyInjector.Interfaces;
using TeenyDependencyInjector.Structures;

namespace TeenyDependencyInjector
{
    public sealed class DependencyService : IDependencyService
    {
        #region Singleton Properties

        /// <summary>
        /// The lazy
        /// </summary>
        private static readonly Lazy<DependencyService> _lazy = new Lazy<DependencyService>(() => new DependencyService());

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static DependencyService Instance => _lazy.Value;

        #endregion

        #region Private Variables

        /// <summary>
        /// The object collection
        /// </summary>
        private readonly ConcurrentBag<BindingStructure> _objects;

        #endregion

        #region Constructor

        public DependencyService()
        {
            _objects = new ConcurrentBag<BindingStructure>();
        }

        #endregion

        /// <summary>
        /// Registers the specific type instance asynchronously. Registering the instance with a name guarantees a unique instance.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TConcrete">The type of the concrete.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public async Task<TInterface> RegisterInstanceAsync<TInterface, TConcrete>(TInterface instance, string name = null) where TConcrete : class, TInterface
        {
            return await Task.Run(() =>
            {
                if (_objects.Any(x => name != null && x.BindingName.ToLowerInvariant() == name.ToLowerInvariant()))
                    throw new DependencyBindingException("Dependency instance already exists");

                _objects.Add(new BindingStructure(typeof(TInterface), typeof(TConcrete), instance, name));

                return instance;
            });
        }

        /// <summary>
        /// Registers the type asynchronously. Parameters are passed through to the constructor of the object upon creation.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TConcrete">The type of the concrete.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public async Task RegisterTypeAsync<TInterface, TConcrete>(params object[] parameters)
        {
            await Task.Run(() =>
            {
                if (_objects.Any(x => x.ConcreteType == typeof(TConcrete)))
                    throw new DependencyBindingException("Concrete type already registered");

                if (_objects.Any(x => x.BindingType == typeof(TInterface)))
                    throw new DependencyBindingException("Interface type already registered");

                _objects.Add(new BindingStructure(typeof(TInterface), typeof(TConcrete), parameters: parameters));
            });
        }

        /// <summary>
        /// Creates a new instance of the specified type asyncronously.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <returns></returns>
        public async Task<TInterface> CreateInstanceAsync<TInterface>() where TInterface : class
        {
            return await Task.Run(() =>
            {
                BindingStructure binding = _objects.FirstOrDefault(x => x.BindingType == typeof(TInterface));
                if (binding.ConcreteType == null || binding.BindingType == null)
                    return default(TInterface);

                return Activator.CreateInstance(binding.ConcreteType, binding.Parameters) as TInterface;
            });
        }

        /// <summary>
        /// Gets the instance asynchronously. If binding name is specified, the service will retrieve the specific instance.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <param name="bindingName">Name of the binding.</param>
        /// <returns></returns>
        public async Task<TInterface> GetInstanceAsync<TInterface>(string bindingName = null) where TInterface : class
        {
            return await Task.Run(() =>
            {
                BindingStructure binding = bindingName == null ? 
                    _objects.FirstOrDefault(x => x.BindingType == typeof(TInterface)) : 
                    _objects.FirstOrDefault(x => x.BindingType == typeof(TInterface) && x.BindingName.Equals(bindingName, StringComparison.OrdinalIgnoreCase));

                if (!(binding.BindingObject is TInterface rval))
                    return default(TInterface);

                return rval;
            });
        }
    }
}
