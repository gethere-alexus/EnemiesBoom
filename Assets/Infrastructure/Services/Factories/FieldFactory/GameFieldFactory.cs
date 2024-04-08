using Infrastructure.AssetsPaths;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.AutoProcessesControl;
using Infrastructure.Services.ConfigLoad;
using Infrastructure.Services.Factories.AnvilFactory;
using Infrastructure.Services.Factories.UI;
using Infrastructure.Services.ProgressLoad;
using Sources.GameFieldBase;
using Sources.GameFieldBase.Extensions.AutoMerge;
using Sources.GameFieldBase.Extensions.ItemsSort;
using Sources.GameFieldBase.Extensions.SlotsUnlock;
using UnityEngine;

namespace Infrastructure.Services.Factories.FieldFactory
{
    public class GameFieldFactory : IGameFieldFactory
    {
        
        private readonly IAssetProvider _assetProvider;
        private readonly IAutoProcessesController _autoProcessesController;
        private readonly IUIFactory _uiFactory;
        private readonly IProgressProvider _progressProvider;
        private readonly IConfigLoader _configLoader;

        private Transform _slotsControl;
        private GameFieldDisplay _gameFieldDisplay;
        private SlotsUnlocker _slotsUnlocker;

        public GameFieldFactory(IAssetProvider assetProvider, IAutoProcessesController autoProcessesController, IUIFactory uiFactory, IProgressProvider progressProvider, IConfigLoader configLoader)
        {
            _assetProvider = assetProvider;
            _autoProcessesController = autoProcessesController;
            _uiFactory = uiFactory;
            _progressProvider = progressProvider;
            _configLoader = configLoader;
        }
        
        public void CreateField()
        {
            _gameFieldDisplay = _assetProvider.Instantiate<GameFieldDisplay>(SlotPaths.SlotsHolderUI, _uiFactory.UIRoot.transform);
            _gameFieldDisplay.Construct(_assetProvider, _progressProvider);
            
            _progressProvider.RegisterObserver(_gameFieldDisplay.GameFieldInstance);
        }
        
        public void CreateFieldExtensions()
        {
            ConstructSlotsUnlocker();
            ConstructSlotsSorter();
            ConstructAutoMerge();
        }
        
        public void CreateFieldControl()
        {
            if (_gameFieldDisplay == null)
                CreateField();

            _slotsControl = _assetProvider.Instantiate(SlotPaths.SlotsControlUI, _gameFieldDisplay.transform).transform;
            CreateAnvil();
        }

        private void CreateAnvil()
        {
            IAnvilFactory anvilFactory = new AnvilFactory.AnvilFactory(_progressProvider,_gameFieldDisplay.GameFieldInstance, _slotsControl.gameObject, _autoProcessesController);
            anvilFactory.CreateAnvil();
            anvilFactory.CreateAnvilExtensions();
        }
        

        private void ConstructSlotsUnlocker()
        {
            _slotsUnlocker = _gameFieldDisplay.GetComponentInChildren<SlotsUnlocker>();
            _slotsUnlocker.Construct(_gameFieldDisplay.GameFieldInstance);
            
            _configLoader.RegisterLoader(_slotsUnlocker);
        }

        /// <summary>
        /// Instantiates and constructs auto-merge extension.
        /// </summary>
        private void ConstructAutoMerge()
        {
            AutoSlotsMerger autoMerge = _gameFieldDisplay.GetComponentInChildren<AutoSlotsMerger>();
            autoMerge.Construct(_gameFieldDisplay.GameFieldInstance);
            
            _progressProvider.RegisterObserver(autoMerge);
            _autoProcessesController.RegisterController(autoMerge);
        }

        /// <summary>
        /// Instantiates and constructs item sorter extension.
        /// </summary>
        private void ConstructSlotsSorter()
        {
            ItemsSorter itemsSorter = _slotsControl.GetComponentInChildren<ItemsSorter>();
            itemsSorter.Construct(_gameFieldDisplay.GameFieldInstance, ItemsComparer.CompareFromHighLevelToLow);
        }
        
        public SlotsUnlocker SlotsUnlocker => _slotsUnlocker;
        public GameFieldDisplay GameFieldDisplay => _gameFieldDisplay;
    }
}