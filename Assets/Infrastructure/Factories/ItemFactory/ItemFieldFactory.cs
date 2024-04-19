using Infrastructure.Factories.AnvilFactories;
using Infrastructure.Factories.UI;
using Infrastructure.PrefabPaths;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.AutoPlayControl;
using Infrastructure.Services.ConfigLoad;
using Infrastructure.Services.ProgressLoad;
using Infrastructure.Services.UpgradeRegistry;
using Sources.ItemsBase.ItemBase;
using Sources.ItemsBase.ItemFieldBase;
using Sources.ItemsBase.ItemFieldBase.Extensions.AutoMerge;
using Sources.ItemsBase.ItemFieldBase.Extensions.ItemsSort;
using Sources.ItemsBase.ItemFieldBase.Extensions.SlotsUnlock;
using Sources.ItemsBase.ItemSlotBase;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factories.ItemFactory
{
    public class ItemFieldFactory : IItemFieldFactory
    {
        
        private readonly IAssetProvider _assetProvider;
        private readonly IAutoPlayController _autoPlayController;
        private readonly IUIRootFactory _uiRootFactory;
        private readonly IProgressProvider _progressProvider;
        private readonly IConfigProvider _configProvider;
        private readonly DiContainer _instanceRegistry;

        private readonly IAnvilFactory _anvilFactory;
        
        private ItemFieldDisplay _itemFieldDisplay;
        private readonly ItemField _itemField;
        private FieldControl _fieldControl;

        public ItemFieldFactory(IAssetProvider assetProvider, IAutoPlayController autoPlayController, 
            IUIRootFactory uiRootFactory, IProgressProvider progressProvider, IConfigProvider configProvider, DiContainer instanceRegistry)
        {
            _assetProvider = assetProvider;
            _autoPlayController = autoPlayController;
            _uiRootFactory = uiRootFactory;
            _progressProvider = progressProvider;
            _configProvider = configProvider;
            _instanceRegistry = instanceRegistry;

            _itemField = new ItemField();
            _anvilFactory = new AnvilFactory(this, progressProvider, autoPlayController,instanceRegistry, _itemField);
        }

        public void CreateItemField()
        {
            CreateField();
            CreateFieldControl();
            CreateFieldExtensions();
        }

        private void CreateField()
        {
            _itemFieldDisplay = _assetProvider.Instantiate<ItemFieldDisplay>(GameFieldPaths.FieldUI, _uiRootFactory.UIRoot.transform);
            _itemFieldDisplay.Construct(_itemField);
            
            InstantiateFieldDisplay(_itemFieldDisplay.ItemFieldInstance.Grid);

            _instanceRegistry.Bind<ItemField>().FromInstance(_itemFieldDisplay.ItemFieldInstance).AsSingle();
            _progressProvider.RegisterObserver(_itemFieldDisplay.ItemFieldInstance);
        }

        private void InstantiateFieldDisplay(ItemSlot[] grid)
        {
            ItemField itemField = _itemFieldDisplay.ItemFieldInstance;
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
            if (_uiRootFactory.UIRoot == null)
                CreateField();
            
            CreateAnvil();
        }

        private void CreateAnvil()
        {
            _anvilFactory.CreateAnvil();
            _anvilFactory.CreateAnvilExtensions();
        }
        

        private void ConstructSlotsUnlocker()
        {
            SlotsUnlocker slotsUnlocker = _itemFieldDisplay.GetComponentInChildren<SlotsUnlocker>();
            slotsUnlocker.Construct(_itemFieldDisplay.ItemFieldInstance);
            
            _configProvider.RegisterLoader(slotsUnlocker);
        }

        /// <summary>
        /// Instantiates and constructs auto-merge extension.
        /// </summary>
        private void ConstructAutoMerge()
        {
            AutoSlotsMerger autoMerge = _itemFieldDisplay.GetComponentInChildren<AutoSlotsMerger>();
            autoMerge.Construct(_itemFieldDisplay.ItemFieldInstance);
            
            _progressProvider.RegisterObserver(autoMerge);
            _autoPlayController.RegisterAutoPlay(autoMerge);
            
            _instanceRegistry.Bind<AutoSlotsMerger>().FromInstance(autoMerge).AsSingle();
        }

        /// <summary>
        /// Instantiates and constructs item sorter extension.
        /// </summary>
        private void ConstructSlotsSorter()
        {
            ItemsSorter itemsSorter = _fieldControl.GetComponentInChildren<ItemsSorter>();
            itemsSorter.Construct(_itemFieldDisplay.ItemFieldInstance, ItemsComparer.CompareFromHighLevelToLow);
        }
        private FieldControl InstantiateFieldControl()
        {
            _fieldControl = _assetProvider.Instantiate<FieldControl>(GameFieldPaths.FieldControlUI,
                _uiRootFactory.UIRoot.transform);
            return _fieldControl;
        }

        public FieldControl FieldControl => _fieldControl ? _fieldControl : InstantiateFieldControl();
    }
}