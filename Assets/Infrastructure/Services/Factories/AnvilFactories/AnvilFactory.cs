using Infrastructure.Services.AutoProcessesControl;
using Infrastructure.Services.ConfigLoad;
using Infrastructure.Services.ProgressLoad;
using Infrastructure.Services.UpgradeRegistry;
using Sources.AnvilBase;
using Sources.AnvilBase.AnvilExtensions.AutoRefiller;
using Sources.AnvilBase.AnvilExtensions.AutoUse;
using Sources.AnvilBase.AnvilExtensions.ChargesRefiller;
using Sources.ItemsBase.ItemFieldBase;
using UnityEngine;

namespace Infrastructure.Services.Factories.AnvilFactories
{
    public class AnvilFactory : IAnvilFactory
    {
        private readonly IProgressProvider _progressProvider;
        private readonly IUpgradesRegistry _upgradesRegistry;
        private readonly IConfigLoader _configLoader;
        private readonly ItemField _itemField;
        private readonly GameObject _slotsControl;
        private readonly IAutoProcessesController _autoProcessesController;

        private Anvil _anvilInstance;

        public AnvilFactory(IProgressProvider progressProvider, IUpgradesRegistry upgradesRegistry, IConfigLoader configLoader, ItemField itemField, GameObject slotsControl,
            IAutoProcessesController autoProcessesController)
        {
            _progressProvider = progressProvider;
            _upgradesRegistry = upgradesRegistry;
            _configLoader = configLoader;
            _itemField = itemField;
            _slotsControl = slotsControl;
            _autoProcessesController = autoProcessesController;
        }

        public void CreateAnvil()
        {
            Anvil anvilInstance = new Anvil(_itemField);
            
            AnvilDisplay display = _slotsControl.GetComponentInChildren<AnvilDisplay>();
            display.Construct(anvilInstance);
            _anvilInstance = display.AnvilInstance;
            
            _configLoader.RegisterLoader(anvilInstance);
            _upgradesRegistry.Register(anvilInstance);
            _progressProvider.RegisterObserver(_anvilInstance);
        }

        public void CreateAnvilExtensions()
        {
            ConstructAnvilAutoUsing();
            ConstructAnvilRefiller();
            ConstructAnvilAutoRefiller();
        }
        private void ConstructAnvilAutoUsing()
        {
            AnvilAutoUse autoUsing = _slotsControl.GetComponentInChildren<AnvilAutoUse>();
            autoUsing.Construct(_anvilInstance);
            
            _progressProvider.RegisterObserver(autoUsing);
            _autoProcessesController.RegisterController(autoUsing);
        }

        private void ConstructAnvilAutoRefiller()
        {
            AnvilAutoRefiller autoRefiller = _slotsControl.GetComponentInChildren<AnvilAutoRefiller>();
            autoRefiller.Construct(_anvilInstance);
            
            _progressProvider.RegisterObserver(autoRefiller);
            _autoProcessesController.RegisterController(autoRefiller);
        }

        private void ConstructAnvilRefiller()
        {
            AnvilChargesRefillDisplay refillDisplay = _slotsControl.GetComponentInChildren<AnvilChargesRefillDisplay>();
            refillDisplay.Construct(_anvilInstance);
            
            _progressProvider.RegisterObserver(refillDisplay.ChargesRefillInstance);
        }

        public Anvil Anvil => _anvilInstance;
    }
}