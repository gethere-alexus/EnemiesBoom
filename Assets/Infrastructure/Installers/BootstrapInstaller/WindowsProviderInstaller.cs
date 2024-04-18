using Infrastructure.Factories.UI;
using Infrastructure.Factories.Windows;
using Infrastructure.Services.PrefabLoad;
using Infrastructure.Services.WindowProvider;
using Zenject;

namespace Infrastructure.Installers.BootstrapInstaller
{
    public class WindowsProviderInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<WindowsProviderInstaller>().FromInstance(this);
        }

        public void Initialize()
        {
            IPrefabLoader prefabLoader = Container.Resolve<IPrefabLoader>();
            IUIRootFactory uiRootFactory = Container.Resolve<IUIRootFactory>();
            
            IWindowsFactory windowsFactory = 
                InstallWindowsFactory(uiRootFactory, prefabLoader);
            IWindowsProvider windowsProvider = 
                InstallWindowsProvider(windowsFactory);
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
    }
}