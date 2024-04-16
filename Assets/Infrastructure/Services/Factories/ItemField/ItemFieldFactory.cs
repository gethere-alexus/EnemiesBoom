using Infrastructure.PrefabPaths;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.AutoProcessesControl;
using Infrastructure.Services.ConfigLoad;
using Infrastructure.Services.Factories.AnvilFactories;
using Infrastructure.Services.Factories.UI;
using Infrastructure.Services.ProgressLoad;
using Sources.ItemsBase.ItemBase;
using Sources.ItemsBase.ItemFieldBase;
using Sources.ItemsBase.ItemFieldBase.Extensions.AutoMerge;
using Sources.ItemsBase.ItemFieldBase.Extensions.ItemsSort;
using Sources.ItemsBase.ItemFieldBase.Extensions.SlotsUnlock;
using Sources.ItemsBase.ItemSlotBase;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Factories.ItemField
{
    public class ItemFieldFactory : IItemFieldFactory
    {
        
        private readonly IAssetProvider _assetProvider;
        private readonly IAutoProcessesController _autoProcessesController;
        private readonly IUIRootFactory _uiFactory;
        private readonly IProgressProvider _progressProvider;
        private readonly IConfigLoader _configLoader;
        private readonly DiContainer _instanceRegistry;

        private ItemFieldDisplay _itemFieldDisplay;
        private Transform _slotsControl;
        
        public ItemFieldFactory(IAssetProvider assetProvider, IAutoProcessesController autoProcessesController, 
            IUIRootFactory uiFactory, IProgressProvider progressProvider, IConfigLoader configLoader, DiContainer instanceRegistry)
        {
            _assetProvider = assetProvider;
            _autoProcessesController = autoProcessesController;
            _uiFactory = uiFactory;
            _progressProvider = progressProvider;
            _configLoader = configLoader;
            _instanceRegistry = instanceRegistry;
        }

        public void CreateItemField()
        {
            CreateField();
            CreateFieldControl();
            CreateFieldExtensions();
        }

        private void CreateField()
        {
            _itemFieldDisplay = _assetProvider.Instantiate<ItemFieldDisplay>(GameFieldPaths.FieldUI, _uiFactory.UIRoot.transform);
            _itemFieldDisplay.Construct();
            
            InstantiateFieldDisplay(_itemFieldDisplay.ItemFieldInstance.Grid);

            _instanceRegistry.Bind<Sources.ItemsBase.ItemFieldBase.ItemField>().FromInstance(_itemFieldDisplay.ItemFieldInstance).AsSingle();
            _progressProvider.RegisterObserver(_itemFieldDisplay.ItemFieldInstance);
        }

        private void InstantiateFieldDisplay(ItemSlot[] grid)
        {
            Sources.ItemsBase.ItemFieldBase.ItemField itemField = _itemFieldDisplay.ItemFieldInstance;
            ItemDrag itemDrag = _itemFieldDisplay.ItemDrag;
            
            for (int i = 0; i < grid.Length; i++)
            {
                ItemSlotDisplay itemSlotDisplay = _assetProvider.Instantiate<ItemSlotDisplay>(GameFieldPaths.SlotTemplate, _itemFieldDisplay.SlotsStorage);
                itemSlotDisplay.Construct(_assetProvider, itemField, itemDrag);
                
                itemField.Grid[i] = itemSlotDisplay.ItemSlotInstance;
                itemField.Grid[i].SlotMerging += itemField.TryMergeItems;
            }
        }

        public void CreateFieldExtensions()
        {
            ConstructSlotsUnlocker();
            ConstructSlotsSorter();
            ConstructAutoMerge();
        }

        public void CreateFieldControl()
        {
            if (_itemFieldDisplay == null)
                CreateField();

            _slotsControl = _assetProvider.Instantiate(GameFieldPaths.FieldControlUI, _itemFieldDisplay.transform).transform;
            CreateAnvil();
        }

        private void CreateAnvil()
        {
            IAnvilFactory anvilFactory = new AnvilFactory(_progressProvider,_itemFieldDisplay.ItemFieldInstance, _slotsControl.gameObject, _autoProcessesController);
            anvilFactory.CreateAnvil();
            anvilFactory.CreateAnvilExtensions();
        }
        

        private void ConstructSlotsUnlocker()
        {
            SlotsUnlocker slotsUnlocker = _itemFieldDisplay.GetComponentInChildren<SlotsUnlocker>();
            slotsUnlocker.Construct(_itemFieldDisplay.ItemFieldInstance);
            
            _configLoader.RegisterLoader(slotsUnlocker);
        }

        /// <summary>
        /// Instantiates and constructs auto-merge extension.
        /// </summary>
        private void ConstructAutoMerge()
        {
            AutoSlotsMerger autoMerge = _itemFieldDisplay.GetComponentInChildren<AutoSlotsMerger>();
            autoMerge.Construct(_itemFieldDisplay.ItemFieldInstance);
            
            _progressProvider.RegisterObserver(autoMerge);
            _autoProcessesController.RegisterController(autoMerge);
        }

        /// <summary>
        /// Instantiates and constructs item sorter extension.
        /// </summary>
        private void ConstructSlotsSorter()
        {
            ItemsSorter itemsSorter = _slotsControl.GetComponentInChildren<ItemsSorter>();
            itemsSorter.Construct(_itemFieldDisplay.ItemFieldInstance, ItemsComparer.CompareFromHighLevelToLow);
        }
    }
}