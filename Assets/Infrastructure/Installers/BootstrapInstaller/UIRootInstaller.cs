using Infrastructure.Factories.UI;
using Infrastructure.Services.AssetsProvider;
using Zenject;

namespace Infrastructure.Installers.BootstrapInstaller
{
    public class UIRootInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<UIRootInstaller>().FromInstance(this);
        }

        public void Initialize()
        {
            IAssetProvider assetProvider = Container.Resolve<IAssetProvider>();
            
            IUIRootFactory uiRootFactory = 
                InstallUIRootFactory(assetProvider);
        }
        private IUIRootFactory InstallUIRootFactory(IAssetProvider assetProvider)
        {
            IUIRootFactory uiRootFactory = new UIRootFactory(assetProvider);
            Container.Bind<IUIRootFactory>().FromInstance(uiRootFactory).AsSingle();
            return uiRootFactory;
        }
    }
}