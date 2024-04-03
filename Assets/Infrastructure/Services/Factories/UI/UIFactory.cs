using Infrastructure.Services.AssetsProvider;
using UnityEngine;

namespace Infrastructure.Services.Factories.UI
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;

        private Canvas _uiRoot;

        public UIFactory(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public Canvas CreateUIRoot()
        {
            _uiRoot = _assetProvider.Instantiate<Canvas>(AssetPaths.UIRoot);
            return _uiRoot;
        }
        

        public Canvas UIRoot => 
            _uiRoot ?? CreateUIRoot();
    }
}