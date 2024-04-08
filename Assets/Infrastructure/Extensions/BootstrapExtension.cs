using Infrastructure.Services;
using Zenject;

namespace Infrastructure.Extensions
{
    public static class BootstrapExtension
    {
        public static TService RegisterService<TService>(this TService instance, DiContainer container)
            where TService : IService
        {
            container.Bind<TService>().FromInstance(instance).AsSingle().NonLazy();
            return instance;
        }
        
    }
}