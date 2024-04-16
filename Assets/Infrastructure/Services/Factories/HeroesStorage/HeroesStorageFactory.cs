using Infrastructure.PrefabPaths;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.ConfigLoad;
using Infrastructure.Services.Factories.UIFactory;
using Infrastructure.Services.ProgressLoad;
using Sources.Hero;
using Sources.Hero.HeroesFieldBase;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Factories.HeroesStorage
{
    public class HeroesStorageFactory : IHeroesStorageFactory
    {
        private readonly IUIRootFactory _uiFactory;
        private readonly IAssetProvider _assetProvider;
        private readonly IConfigLoader _configLoader;
        private readonly IProgressProvider _progressProvider;
        private readonly DiContainer _instanceRegistry;
        
        public HeroesStorageFactory(IUIRootFactory uiFactory, IAssetProvider assetProvider, IConfigLoader configLoader, IProgressProvider progressProvider, DiContainer instanceRegistry)
        {
            _uiFactory = uiFactory;
            _assetProvider = assetProvider;
            _configLoader = configLoader;
            _progressProvider = progressProvider;
            _instanceRegistry = instanceRegistry;
        }
        

        public void CreateHeroesField()
        {
            HeroesFieldDisplay heroesFieldDisplay =
                _assetProvider.InstantiateFromZenject<HeroesFieldDisplay>(HeroPaths.HeroesStoragePath,
                    _uiFactory.UIRoot.transform);

            _instanceRegistry.Bind<HeroesField>().FromInstance(heroesFieldDisplay.HeroesFieldInstance)
                .AsSingle();
            
            _configLoader.RegisterLoader(heroesFieldDisplay.HeroesFieldInstance);
            _progressProvider.RegisterObserver(heroesFieldDisplay.HeroesFieldInstance);
        }

        public void CreateHeroesInventory()
        {
            HeroesInventory heroesInventory = new HeroesInventory();
        }
    }
}