using Infrastructure.PrefabPaths;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.WindowProvider;
using Sources.MenuBase;

namespace Infrastructure.Factories.UI
{
    public class UIMenuFactory : IUIMenuFactory
    {
        private readonly IWindowsProvider _windowProvider;
        private readonly IAssetProvider _assetProvider;
        private readonly IUIRootFactory _uiRootFactory;

        public UIMenuFactory(IWindowsProvider windowProvider, IAssetProvider assetProvider, IUIRootFactory uiRootFactory)
        {
            _windowProvider = windowProvider;
            _assetProvider = assetProvider;
            _uiRootFactory = uiRootFactory;
        }

        public void CreateBottomMenu()
        {
            
            BottomMenu menu = _assetProvider.Instantiate<BottomMenu>(UIPaths.BottomMenu, _uiRootFactory.UIRoot.transform);
            menu.Construct(_windowProvider);
        }
    }
}