using System.Threading.Tasks;

namespace TeenyDependencyInjector.Interfaces
{
    public interface IDependencyService
    {
        Task RegisterInstanceAsync<TInterface, TConcrete>(TInterface instance, string name = null) where TConcrete: class, TInterface;
        Task RegisterTypeAsync<TInterface, TConcrete>(params object[] parameters);

        Task<TInterface> CreateInstanceAsync<TInterface>() where TInterface : class;
        Task<TInterface> GetInstanceAsync<TInterface>(string bindingName = null) where TInterface : class;
    }
}
