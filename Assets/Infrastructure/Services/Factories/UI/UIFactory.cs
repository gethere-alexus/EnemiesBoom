using Infrastructure.AssetsPaths;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.WindowProvider;
using Sources.Menu;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Factories.UI
{
    public class UIFactory : IUIFactory
    {
        private readonly DiContainer _diContainer;
        private readonly IAssetProvider _assetProvider;

        private Canvas _uiRoot;

        public UIFactory(DiContainer diContainer, IAssetProvider assetProvider)
        {
            _diContainer = diContainer;
            _assetProvider = assetProvider;
        }

        public Canvas CreateUIRoot()
        {
            _uiRoot = _assetProvider.Instantiate<Canvas>(UIPaths.UIRoot);
            return _uiRoot;
        }

        public void CreateBottomMenu()
        {
            BottomMenu menu = _assetProvider.Instantiate<BottomMenu>(UIPaths.BottomMenu, UIRoot.transform);
            menu.Construct(_diContainer.Resolve<IWindowsProvider>());
        }


        public Canvas UIRoot => 
            _uiRoot == null ? CreateUIRoot() : _uiRoot;
    }
}