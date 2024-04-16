using Infrastructure.PrefabPaths;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.WindowProvider;
using UnityEngine;

namespace Infrastructure.Services.Factories.UIFactory
{
    public class UIRootFactory : IUIRootFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IWindowsProvider _windowsProvider;

        private Canvas _uiRoot;

       
        public UIRootFactory(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public Canvas CreateUIRoot()
        {
            _uiRoot = _assetProvider.Instantiate<Canvas>(UIPaths.UIRoot);
            return _uiRoot;
        }

        // public void CreateBottomMenu()
        // {
        //     IWindowsProvider windowsProvider = _diContainer.Resolve<IWindowsProvider>();
        //     
        //     BottomMenu menu = _assetProvider.Instantiate<BottomMenu>(UIPaths.BottomMenu, UIRoot.transform);
        //     menu.Construct(windowsProvider);
        // }


        public Canvas UIRoot => 
            _uiRoot == null ? CreateUIRoot() : _uiRoot;
    }
}