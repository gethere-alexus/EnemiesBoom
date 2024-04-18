using Infrastructure.Services.ConfigLoad;
using Infrastructure.Services.PrefabLoad;
using Infrastructure.Services.ProgressLoad;
using Infrastructure.Services.UpgradeRegistry;
using Zenject;

namespace Infrastructure.Installers.BootstrapInstaller
{
    public class UpgradesRegistryInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<UpgradesRegistryInstaller>().FromInstance(this);
        }

        public void Initialize()
        {
            IProgressProvider progressProvider = Container.Resolve<IProgressProvider>();
            IConfigProvider configProvider = Container.Resolve<IConfigProvider>();
            IPrefabLoader prefabLoader = Container.Resolve<IPrefabLoader>();
            
            IUpgradesRegistry upgradesRegistry = new UpgradesRegistry(progressProvider,configProvider,prefabLoader,Container);
            Container.Bind<IUpgradesRegistry>().FromInstance(upgradesRegistry).AsSingle();
        }
    }
}