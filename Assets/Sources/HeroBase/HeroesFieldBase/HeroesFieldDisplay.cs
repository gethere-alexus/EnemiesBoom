using Infrastructure.PrefabPaths;
using Infrastructure.Services.AssetsProvider;
using Sources.HeroBase.HeroSlotBase;
using Sources.ItemsBase.ItemFieldBase;
using UnityEngine;
using Zenject;

namespace Sources.HeroBase.HeroesFieldBase
{
    public class HeroesFieldDisplay : MonoBehaviour
    {
        [SerializeField] private Transform _heroesHolder;
        
        private IAssetProvider _assetProvider;
        private HeroesField _heroesFieldInstance;

        [Inject]
        public void Construct(IAssetProvider assetProvider, HeroesInventory inventory, ItemField itemField)
        {
            _assetProvider = assetProvider;
            
            _heroesFieldInstance = new HeroesField(inventory, itemField);
            _heroesFieldInstance.ConfigLoaded += BuildHeroesFields;
        }

        private void BuildHeroesFields()
        {
            ClearStorage();
            InstantiateHolders();
        }

        private void InstantiateHolders()
        {
            for (int i = 0; i < _heroesFieldInstance.ActiveHeroesSlots.Length; i++)
            {
                var activeHeroDisplay = _assetProvider.Instantiate<HeroSlotDisplay>
                    (HeroPaths.HeroSlot, _heroesHolder);
                
                activeHeroDisplay.Construct(_heroesFieldInstance.ActiveHeroesSlots[i]);
            }
        }

        private void ClearStorage()
        {
            foreach (Transform child in _heroesHolder)
            {
                Destroy(child.gameObject);
            }
        }

        private void OnDisable() => 
            _heroesFieldInstance.ConfigLoaded -= BuildHeroesFields;

        public HeroesField HeroesFieldInstance => _heroesFieldInstance;
    }
}