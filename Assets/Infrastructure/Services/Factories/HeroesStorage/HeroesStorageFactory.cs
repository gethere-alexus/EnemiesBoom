using Infrastructure.AssetsPaths;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.ConfigLoad;
using Infrastructure.Services.Factories.UI;
using Sources.HeroBase.Storing.Holder;

namespace Infrastructure.Services.Factories.HeroesStorage
{
    public class HeroesStorageFactory : IHeroesStorageFactory
    {
        
        private readonly IUIFactory _uiFactory;
        private readonly IAssetProvider _assetProvider;
        private readonly IConfigLoader _configLoader;

        private HeroSlotsHolderDisplay _heroSlotsHolderDisplay;

        public HeroesStorageFactory(IUIFactory uiFactory, IAssetProvider assetProvider, IConfigLoader configLoader)
        {
            _uiFactory = uiFactory;
            _assetProvider = assetProvider;
            _configLoader = configLoader;
        }

        public void CreateActiveHeroesStorage()
        {
            _heroSlotsHolderDisplay = _assetProvider.Instantiate<HeroSlotsHolderDisplay>(HeroPaths.HeroesStoragePath, _uiFactory.UIRoot.transform);
            _heroSlotsHolderDisplay.Construct(_assetProvider);
            
            _configLoader.RegisterLoader(_heroSlotsHolderDisplay.HeroSlotsHolderInstance);
        }
    }
}