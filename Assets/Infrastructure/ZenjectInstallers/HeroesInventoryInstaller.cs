using Infrastructure.Services.ConfigLoad;
using Sources.Hero;
using Zenject;

namespace Infrastructure.ZenjectInstallers
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
            HeroesInventory inventory = new HeroesInventory();
            Container.Resolve<IConfigLoader>()
                .RegisterLoader(inventory);
            Container.Bind<HeroesInventory>().FromInstance(inventory).AsSingle();
        }
    }
}