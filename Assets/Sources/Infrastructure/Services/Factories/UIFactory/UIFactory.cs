using Sources.Infrastructure.Services.AssetsProvider;
using UnityEngine;
using Zenject;

namespace Sources.Infrastructure.Services.Factories.UIFactory
{
    // Factory where the ui elements are being created
    public class UIFactory : IUIFactory 
    {
        private readonly IAssetProvider _assetProvider;
        private readonly DiContainer _diContainer;
        private Canvas _uiRoot;
        public UIFactory(IAssetProvider assetProvider, DiContainer diContainer)
        {
            _assetProvider = assetProvider;
            _diContainer = diContainer;
        }

        public void CreateUIRoot() =>
            _uiRoot = _assetProvider.Instantiate<Canvas>(AssetPaths.UIRoot);

        public void CreateSlotsUI()
        {
            if(_uiRoot == null)
                CreateUIRoot();

            var slotsHolder = _diContainer.InstantiatePrefab
                (_assetProvider.LoadPrefab(AssetPaths.SlotsHolderUI),_uiRoot.transform);
        }
    }
}