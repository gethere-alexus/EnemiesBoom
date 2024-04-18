using Infrastructure.Factories.HeroesStorage;
using Infrastructure.Factories.ItemFactory;
using Infrastructure.Factories.UI;
using Infrastructure.Factories.Wallet;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.AutoProcessesControl;
using Infrastructure.Services.ConfigLoad;
using Infrastructure.Services.ProgressLoad;
using Infrastructure.Services.UpgradeRegistry;
using Infrastructure.Services.WindowProvider;
using Zenject;

namespace Infrastructure.Installers.BootstrapInstaller
{
    public class FactoriesInstaller : MonoInstaller, IInitializable
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings() => 
            Container.BindInterfacesTo<FactoriesInstaller>().FromInstance(this);

        public void Initialize()
        {
            IAssetProvider assetProvider = Container.Resolve<IAssetProvider>();
            IProgressProvider progressProvider = Container.Resolve<IProgressProvider>();
            IAutoProcessesController autoProcessesController = Container.Resolve<IAutoProcessesController>();
            IConfigProvider configProvider = Container.Resolve<IConfigProvider>();
            IWindowsProvider windowsProvider = Container.Resolve<IWindowsProvider>();
            IUIRootFactory uiRootFactory = Container.Resolve<IUIRootFactory>();
            
            IUIMenuFactory uiMenuFactory = 
                InstallUIMenuFactory(windowsProvider, assetProvider, uiRootFactory);
            
            IItemFieldFactory itemFieldFactory = 
                InstallGameFieldFactory(assetProvider, autoProcessesController, uiRootFactory, progressProvider, configProvider, Container);
            
            IWalletFactory walletFactory = 
                InstallWalletFactory(uiRootFactory, assetProvider, progressProvider, Container);
            
            IHeroesStorageFactory heroesStorageFactory = 
                InstallHeroesStorageFactory(uiRootFactory, assetProvider, configProvider, progressProvider);
            
        }
        
        private IUIMenuFactory InstallUIMenuFactory(IWindowsProvider windowsProvider, IAssetProvider assetProvider,
            IUIRootFactory uiRootFactory)
        {
            IUIMenuFactory uiMenuFactory = new UIMenuFactory(windowsProvider, assetProvider, uiRootFactory);
            Container.Bind<IUIMenuFactory>().FromInstance(uiMenuFactory).AsSingle();
            return uiMenuFactory;
        }
        private IHeroesStorageFactory InstallHeroesStorageFactory(IUIRootFactory uiRootFactory, IAssetProvider assetProvider,
            IConfigProvider configProvider, IProgressProvider progressProvider)
        {
            IHeroesStorageFactory heroesStorageFactory = new HeroesStorageFactory(uiRootFactory, assetProvider, configProvider, progressProvider, Container);
            Container.Bind<IHeroesStorageFactory>().FromInstance(heroesStorageFactory).AsSingle();
            return heroesStorageFactory;
        }

        private IWalletFactory InstallWalletFactory(IUIRootFactory uiRootFactory, IAssetProvider assetProvider, IProgressProvider progressProvider, DiContainer container)
        {
            IWalletFactory walletFactory = new WalletFactory(uiRootFactory, assetProvider, progressProvider, container);
            Container.Bind<IWalletFactory>().FromInstance(walletFactory).AsSingle();
            return walletFactory;
        }
        private IItemFieldFactory InstallGameFieldFactory(IAssetProvider assetProvider, IAutoProcessesController autoProcessesController, 
            IUIRootFactory uiRootFactory, IProgressProvider progressProvider, IConfigProvider configProvider, DiContainer container)
        {
            IItemFieldFactory itemFieldFactory = new ItemFieldFactory(assetProvider, autoProcessesController, uiRootFactory, progressProvider, configProvider, container);
            Container.Bind<IItemFieldFactory>().FromInstance(itemFieldFactory).AsSingle();
            
            return itemFieldFactory;
        }
    }
}