using System.Threading.Tasks;

namespace TeenyDependencyInjector.Interfaces
{
    public interface IDependencyService
    {
        /// <summary>
        /// Registers the instance asynchronously.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TConcrete">The type of the concrete.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        Task<TInterface> RegisterInstanceAsync<TInterface, TConcrete>(TInterface instance, string name = null) where TConcrete: class, TInterface;

        /// <summary>
        /// Registers the type asynchronously.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TConcrete">The type of the concrete.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task RegisterTypeAsync<TInterface, TConcrete>(params object[] parameters);

        /// <summary>
        /// Creates the instance asynchronously.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <returns></returns>
        Task<TInterface> CreateInstanceAsync<TInterface>() where TInterface : class;

        /// <summary>
        /// Gets the instance asynchronously.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <param name="bindingName">Name of the binding.</param>
        /// <returns></returns>
        Task<TInterface> GetInstanceAsync<TInterface>(string bindingName = null) where TInterface : class;
    }
}
