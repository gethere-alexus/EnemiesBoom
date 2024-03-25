using Infrastructure.Configurations;
using Infrastructure.Configurations.Anvil;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.ConfigLoad;
using Sources.AnvilBase;
using Sources.AnvilBase.AnvilExtensions;
using Sources.AnvilChargesRefillerBase;
using Sources.SlotsHolderBase;
using Sources.SlotsHolderBase.Extensions.SlotsSort;
using UnityEngine;

namespace Infrastructure.Services.Factories.UIFactory
{
    /// <summary>
    /// Factory is building UI part of the game
    /// </summary>
    public class UIFactory : IUIFactory 
    {
        private const string AnvilConfigsPath = "Anvil";
        private const string SlotsFieldPath = "SlotsField";

        private readonly IAssetProvider _assetProvider;
        private readonly IConfigLoader _configLoader;

        private Canvas _uiRoot;
        private Transform _slotsControl;
        
        private SlotsHolderDisplay _slotsHolder;
        private AnvilDisplay _anvilDisplay;
        public UIFactory(IAssetProvider assetProvider, IConfigLoader configLoader)
        {
            _assetProvider = assetProvider;
            _configLoader = configLoader;
        }

        public void CreateUIRoot() =>
            _uiRoot = _assetProvider.Instantiate<Canvas>(AssetPaths.UIRoot);

        public void CreateSlotsUI()
        {
            if(_uiRoot == null)
                CreateUIRoot();

            _slotsHolder = _assetProvider.Instantiate<SlotsHolderDisplay>(AssetPaths.SlotsHolderUI, _uiRoot.transform);
            
            SlotsFieldConfiguration slotsConfig = _configLoader.LoadConfiguration<SlotsFieldConfiguration>(SlotsFieldPath);
            _slotsHolder.Construct(_assetProvider, slotsConfig);
        }

        public void CreateSlotsControlUI()
        {
            if(_slotsHolder == null)
                CreateSlotsUI();
            
            _slotsControl = _assetProvider.Instantiate(AssetPaths.SlotsControlUI, _slotsHolder.transform).transform;
            
            InstantiateAnvil();
            InstantiateAnvilAutoRefiller();
            InstantiateAnvilRefiller();
            InstantiateSlotsSorter();
        }

        private void InstantiateAnvilAutoRefiller()
        {
            AnvilAutoRefiller autoRefiller = _slotsControl.GetComponentInChildren<AnvilAutoRefiller>();
            AnvilAutoRefillConfig autoRefillConfig = _configLoader.LoadConfiguration<AnvilAutoRefillConfig>(AnvilConfigsPath);
            
           autoRefiller.Construct(_anvilDisplay.AnvilInstance, autoRefillConfig);
        }

        private void InstantiateSlotsSorter()
        {
            SlotsSorter slotsSorter = _slotsControl.GetComponentInChildren<SlotsSorter>();
            slotsSorter.Construct(_slotsHolder.SlotsHolderInstance);
        }

        private void InstantiateAnvilRefiller()
        {
            AnvilChargesRefillDisplay refillDisplay = _slotsControl.GetComponentInChildren<AnvilChargesRefillDisplay>();
            AnvilRefillConfig refillConfig = _configLoader.LoadConfiguration<AnvilRefillConfig>(AnvilConfigsPath);
            
            refillDisplay.Construct(_anvilDisplay.AnvilInstance, refillConfig);
        }

        private void InstantiateAnvil()
        {
            _anvilDisplay = _slotsControl.GetComponentInChildren<AnvilDisplay>();

            AnvilConfig anvilConfig = _configLoader.LoadConfiguration<AnvilConfig>(AnvilConfigsPath);
            _anvilDisplay.Construct(_slotsHolder.SlotsHolderInstance, anvilConfig);
        }
    }
}