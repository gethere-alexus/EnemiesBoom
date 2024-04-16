using Infrastructure.PrefabPaths;
using Infrastructure.ProgressData;
using Infrastructure.ProgressData.Hero;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.PrefabLoad;
using Infrastructure.Services.WindowProvider;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sources.Windows.HeroesInventory
{
    public class HeroesInventoryWindow : WindowBase
    {
        [Header("Components Configuration")]
        [SerializeField] private Transform _heroesStorage;
        [SerializeField] private Button _closeButton;
        
        private HeroCard _heroCardTemplate;
        private HeroBase.HeroesInventory _heroesInventoryInstance;
        private IWindowsProvider _windowsProvider;

        [Inject]
        public void Construct(IWindowsProvider windowsProvider, HeroBase.HeroesInventory inventoryInstance, IAssetProvider assetProvider, IPrefabLoader prefabLoader)
        {
            _windowsProvider = windowsProvider;
            _heroesInventoryInstance = inventoryInstance;
            _heroCardTemplate = 
                prefabLoader.LoadPrefab<HeroCard>(HeroPaths.HeroCard);
        }

        protected override void OnAwake()
        {
            _closeButton.onClick.AddListener(Close);
            ConstructInventoryContent();
        }

        protected override void OnDisabling() => 
            _closeButton.onClick.RemoveAllListeners();

        protected override void Close()
        {
            base.Close();
            Destroy(gameObject);
        }

        private void OnHeroSelected(HeroData heroData) => 
            _windowsProvider.OpenWindow<HeroData>(WindowType.SelectHeroSlot, heroData);

        private void OnHeroPurchased(HeroData heroData)
        {
            _heroesInventoryInstance.PurchaseHeroByID(heroData.ID);
            ConstructInventoryContent();
        }
        
        private void ConstructInventoryContent()
        {
            ClearStorage();
            
            foreach (HeroData heroData in _heroesInventoryInstance.Heroes)
            {
                bool isHeroUnlocked = _heroesInventoryInstance.IsHeroUnlocked(heroData.ID);
                Instantiate(_heroCardTemplate, _heroesStorage)
                    .Construct(heroData, isHeroUnlocked, OnHeroSelected, OnHeroPurchased);
            }
        }

        private void ClearStorage()
        {
            foreach (Transform child in _heroesStorage.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}