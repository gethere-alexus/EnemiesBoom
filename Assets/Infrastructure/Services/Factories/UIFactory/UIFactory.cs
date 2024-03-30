using Infrastructure.Configurations.Anvil;
using Infrastructure.Configurations.SlotsField;
using Infrastructure.ProgressData.AnvilData;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.ConfigLoad;
using Infrastructure.Services.ProgressProvider;
using Sources.AnvilBase;
using Sources.AnvilBase.AnvilExtensions.AutoRefiller;
using Sources.AnvilBase.AnvilExtensions.AutoUse;
using Sources.AnvilBase.AnvilExtensions.ChargesRefiller;
using Sources.SlotsHolderBase;
using Sources.SlotsHolderBase.Extensions.AutoMerge;
using Sources.SlotsHolderBase.Extensions.ItemsSort;
using Sources.SlotsHolderBase.Extensions.SlotsUnlock;
using UnityEngine;

namespace Infrastructure.Services.Factories.UIFactory
{
    /// <summary>
    /// Factory is building UI part of the game
    /// </summary>
    public class UIFactory : IUIFactory 
    {
        private const string AnvilConfigsPath = "Anvil";
        private const string SlotsConfigsPath = "SlotsField";

        private readonly IAssetProvider _assetProvider;
        private readonly IProgressProvider _progressProvider;
        private readonly IConfigLoader _configLoader;

        private Canvas _uiRoot;
        private Transform _slotsControl;
        
        private SlotsHolderDisplay _slotsHolder;
        private AnvilDisplay _anvilDisplay;
        public UIFactory(IAssetProvider assetProvider, IProgressProvider progressProvider, IConfigLoader configLoader)
        {
            _assetProvider = assetProvider;
            _progressProvider = progressProvider;
            _configLoader = configLoader;
        }
        public void CreateUIRoot() =>
            _uiRoot = _assetProvider.Instantiate<Canvas>(AssetPaths.UIRoot);

        /// <summary>
        /// Creates a game field fulfilled with slots, control is created separately.  
        /// </summary>
        public void CreateSlots()
        {
            if(_uiRoot == null)
                CreateUIRoot();

            _slotsHolder = _assetProvider.Instantiate<SlotsHolderDisplay>(AssetPaths.SlotsHolderUI, _uiRoot.transform);
            
            SlotsFieldConfiguration slotsConfig = _configLoader.LoadConfiguration<SlotsFieldConfiguration>(SlotsConfigsPath);
            _slotsHolder.Construct(_assetProvider, slotsConfig);
        }

        /// <summary>
        /// Creates ui of a game field controls
        /// </summary>
        public void CreateSlotsControl()
        {
            if(_slotsHolder == null)
                CreateSlots();
            
            _slotsControl = _assetProvider.Instantiate(AssetPaths.SlotsControlUI, _slotsHolder.transform).transform;
            
            InstantiateAnvil();
            InstantiateAnvilAutoUsing();
            InstantiateAnvilAutoRefiller();
            InstantiateAnvilRefiller();
            
            InstantiateSlotsSorter();
            InstantiateSlotsUnlocker();
            InstantiateAutoMerge();
        }

        /// <summary>
        /// Instantiates and constructs slots-unlocker extenstion
        /// </summary>
        private void InstantiateSlotsUnlocker()
        {
            SlotsUnlocker slotsUnlocker = _slotsHolder.GetComponentInChildren<SlotsUnlocker>();
            SlotsUnlockConfig config = _configLoader.LoadConfiguration<SlotsUnlockConfig>(SlotsConfigsPath);
            
            slotsUnlocker.Construct(_slotsHolder.SlotsHolderInstance, config);
        }

        /// <summary>
        /// Instantiates and constructs auto-merge extension.
        /// </summary>
        private void InstantiateAutoMerge()
        {
            AutoSlotsMerger autoMerge = _slotsHolder.GetComponentInChildren<AutoSlotsMerger>();
            AutoMergerConfig autoMergeConfig = _configLoader.LoadConfiguration<AutoMergerConfig>(SlotsConfigsPath);

            autoMerge.Construct(_slotsHolder.SlotsHolderInstance, autoMergeConfig);
        }

        /// <summary>
        /// Instantiates and constructs anvil auto-using extenstion.
        /// </summary>
        private void InstantiateAnvilAutoUsing()
        {
            AnvilAutoUse autoUsing = _slotsControl.GetComponentInChildren<AnvilAutoUse>();
            AnvilAutoUseConfig autoUsingConfig = _configLoader.LoadConfiguration<AnvilAutoUseConfig>(AnvilConfigsPath);
            
            autoUsing.Construct(_anvilDisplay.AnvilInstance, autoUsingConfig);
        }

        /// <summary>
        /// Instantiates and constructs anvil auto-refiller extension.
        /// </summary>
        private void InstantiateAnvilAutoRefiller()
        {
            AnvilAutoRefiller autoRefiller = _slotsControl.GetComponentInChildren<AnvilAutoRefiller>();
            AnvilAutoRefillConfig autoRefillConfig = _configLoader.LoadConfiguration<AnvilAutoRefillConfig>(AnvilConfigsPath);
            
           autoRefiller.Construct(_anvilDisplay.AnvilInstance, autoRefillConfig);
        }

        /// <summary>
        /// Instantiates and constructs item sorter extension.
        /// </summary>
        private void InstantiateSlotsSorter()
        {
            ItemsSorter itemsSorter = _slotsControl.GetComponentInChildren<ItemsSorter>();
            itemsSorter.Construct(_slotsHolder.SlotsHolderInstance, ItemsComparer.CompareFromHighLevelToLow);
        }

        /// <summary>
        /// Instantiates and constructs anvil refiller extension.
        /// </summary>
        private void InstantiateAnvilRefiller()
        {
            AnvilChargesRefillDisplay refillDisplay = _slotsControl.GetComponentInChildren<AnvilChargesRefillDisplay>();
            AnvilRefillConfig refillConfig = _configLoader.LoadConfiguration<AnvilRefillConfig>(AnvilConfigsPath);
            
            refillDisplay.Construct(_anvilDisplay.AnvilInstance, refillConfig);
        }

        /// <summary>
        /// Instantiates and constructs anvil from save, if no saves - load config
        /// </summary>
        private void InstantiateAnvil()
        {
            _anvilDisplay = _slotsControl.GetComponentInChildren<AnvilDisplay>();

            AnvilProgress progress = _progressProvider.LoadProgress<AnvilProgress>();
            
            if (progress != null)
            {
                _anvilDisplay.Construct(_slotsHolder.SlotsHolderInstance, _progressProvider, progress);
            }
            else
            {
                AnvilConfig anvilConfig = _configLoader.LoadConfiguration<AnvilConfig>(AnvilConfigsPath);
                _anvilDisplay.Construct(_slotsHolder.SlotsHolderInstance, _progressProvider, anvilConfig);
            }
        }
    }
}