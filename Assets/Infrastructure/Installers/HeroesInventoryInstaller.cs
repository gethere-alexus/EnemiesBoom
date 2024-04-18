using Infrastructure.Services.ConfigLoad;
using Sources.HeroBase;
using Zenject;

namespace Infrastructure.Installers
{
    public class HeroesInventoryInstaller : MonoInstaller, IInitializable
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            BindHeroesInventory();
        }

        private void BindHeroesInventory() =>
            Container.BindInterfacesTo<HeroesInventoryInstaller>().FromInstance(this).AsSingle();

        public void Initialize()
        {
            HeroesInventory inventory = Container.Instantiate<HeroesInventory>();
            
            Container.Resolve<IConfigProvider>()
                .RegisterLoader(inventory);
            Container.Bind<HeroesInventory>().FromInstance(inventory).AsSingle();
        }
    }
}