using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.AutoProcessesControl;
using Infrastructure.Services.ConfigLoad;
using Infrastructure.Services.Factories.FieldFactory;
using Infrastructure.Services.Factories.HeroesStorage;
using Infrastructure.Services.Factories.UIFactory;
using Infrastructure.Services.Factories.WindowFactory;
using Infrastructure.Services.PrefabLoad;
using Infrastructure.Services.ProgressLoad;
using Infrastructure.Services.WindowProvider;
using Zenject;

namespace Infrastructure.ZenjectInstallers
{
    public class ServiceInstaller : MonoInstaller
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            IPrefabLoader prefabLoader = 
                InstallPrefabLoader();
            
            IConfigLoader configLoader = 
                InstallConfigLoader(prefabLoader);
            
            IAssetProvider assetProvider = 
                InstallAssetProvider(prefabLoader);
            
            IUIRootFactory uiRootFactory = 
                InstallUIRootFactory(assetProvider);

            IProgressProvider progressProvider = 
                InstallProgressProvider(prefabLoader);

            IAutoProcessesController autoProcessesController = 
                InstallAutoProcessesController();

            IItemFieldFactory itemFieldFactory = 
                InstallGameFieldFactory(assetProvider, autoProcessesController, uiRootFactory, progressProvider, configLoader, Container);
            
            IHeroesStorageFactory heroesStorageFactory = 
                InstallHeroesStorageFactory(uiRootFactory, assetProvider, configLoader, progressProvider);

            IWindowsFactory windowsFactory = 
                InstallWindowsFactory(uiRootFactory, prefabLoader);
            
            IWindowsProvider windowsProvider = 
                InstallWindowsProvider(windowsFactory);

            IUIMenuFactory uiMenuFactory = 
                InstallUIMenuFactory(windowsProvider, assetProvider, uiRootFactory);
        }

        #region Installs

        private IUIMenuFactory InstallUIMenuFactory(IWindowsProvider windowsProvider, IAssetProvider assetProvider,
            IUIRootFactory uiRootFactory)
        {
            IUIMenuFactory uiMenuFactory = new UIMenuFactory(windowsProvider, assetProvider, uiRootFactory);
            Container.Bind<IUIMenuFactory>().FromInstance(uiMenuFactory).AsSingle();
            return uiMenuFactory;
        }

        private IWindowsProvider InstallWindowsProvider(IWindowsFactory windowsFactory)
        {
            IWindowsProvider windowsProvider = new WindowsProvider(windowsFactory);
            Container.Bind<IWindowsProvider>().FromInstance(windowsProvider).AsSingle();
            return windowsProvider;
        }

        private IWindowsFactory InstallWindowsFactory(IUIRootFactory uiRootFactory, IPrefabLoader prefabLoader)
        {
            IWindowsFactory windowsFactory = new WindowsFactory(uiRootFactory, prefabLoader, Container);
            Container.Bind<IWindowsFactory>().FromInstance(windowsFactory).AsSingle();
            return windowsFactory;
        }

        private IHeroesStorageFactory InstallHeroesStorageFactory(IUIRootFactory uiRootFactory, IAssetProvider assetProvider,
            IConfigLoader configLoader, IProgressProvider progressProvider)
        {
            IHeroesStorageFactory heroesStorageFactory = new HeroesStorageFactory(uiRootFactory, assetProvider, configLoader, progressProvider, Container);
            Container.Bind<IHeroesStorageFactory>().FromInstance(heroesStorageFactory).AsSingle();
            return heroesStorageFactory;
        }

        private IAutoProcessesController InstallAutoProcessesController()
        {
            IAutoProcessesController autoProcessesController = new AutoProcessesController();
            Container.Bind<IAutoProcessesController>().FromInstance(autoProcessesController).AsSingle();
            return autoProcessesController;
        }

        private IProgressProvider InstallProgressProvider(IPrefabLoader prefabLoader)
        {
            IProgressProvider progressProvider = new ProgressProvider(prefabLoader);
            Container.Bind<IProgressProvider>().FromInstance(progressProvider).AsSingle();
            return progressProvider;
        }

        private IUIRootFactory InstallUIRootFactory(IAssetProvider assetProvider)
        {
            IUIRootFactory uiRootFactory = new UIRootFactory(assetProvider);
            Container.Bind<IUIRootFactory>().FromInstance(uiRootFactory).AsSingle();
            return uiRootFactory;
        }

        private IAssetProvider InstallAssetProvider(IPrefabLoader prefabLoader)
        {
            IAssetProvider assetProvider = new AssetProvider(prefabLoader, Container);
            Container.Bind<IAssetProvider>().FromInstance(assetProvider).AsSingle();
            return assetProvider;
        }

        private IConfigLoader InstallConfigLoader(IPrefabLoader prefabLoader)
        {
            IConfigLoader configLoader = new ConfigLoader(prefabLoader);
            Container.Bind<IConfigLoader>().FromInstance(configLoader).AsSingle();
            return configLoader;
        }

        private IPrefabLoader InstallPrefabLoader()
        {
            IPrefabLoader prefabLoader = new PrefabLoader();
            Container.Bind<IPrefabLoader>().FromInstance(prefabLoader).AsSingle();
            return prefabLoader;
        }

        private IItemFieldFactory InstallGameFieldFactory(IAssetProvider assetProvider,
            IAutoProcessesController autoProcessesController, IUIRootFactory uiRootFactory, 
            IProgressProvider progressProvider, IConfigLoader configLoader, DiContainer container)
        {
            IItemFieldFactory itemFieldFactory = new ItemFieldFactory(assetProvider, autoProcessesController, uiRootFactory, progressProvider, configLoader, container);
            Container.Bind<IItemFieldFactory>().FromInstance(itemFieldFactory).AsSingle();
            
            return itemFieldFactory;
        }

        #endregion
    }
}