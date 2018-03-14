using System.Threading.Tasks;

namespace TeenyDependencyInjector.Interfaces
{
    public interface IDependencyService
    {
        /// <summary>
        /// Registers the instance asynchronous.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TConcrete">The type of the concrete.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        Task<TInterface> RegisterInstanceAsync<TInterface, TConcrete>(TInterface instance, string name = null) where TConcrete: class, TInterface;

        Task RegisterTypeAsync<TInterface, TConcrete>(params object[] parameters);

        Task<TInterface> CreateInstanceAsync<TInterface>() where TInterface : class;
        Task<TInterface> GetInstanceAsync<TInterface>(string bindingName = null) where TInterface : class;
    }
}
