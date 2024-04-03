using Infrastructure.Services.AutoProcessesControll;
using Infrastructure.Services.ProgressLoad;
using Sources.AnvilBase;
using Sources.AnvilBase.AnvilExtensions.AutoRefiller;
using Sources.AnvilBase.AnvilExtensions.AutoUse;
using Sources.AnvilBase.AnvilExtensions.ChargesRefiller;
using Sources.GameFieldBase;
using UnityEngine;

namespace Infrastructure.Services.Factories.AnvilFactory
{
    public class AnvilFactory : IAnvilFactory
    {
        private readonly IProgressProvider _progressProvider;
        private readonly GameField _gameField;
        private readonly GameObject _slotsControl;
        private readonly IAutoProcessesController _autoProcessesController;

        private Anvil _anvilInstance;

        public AnvilFactory(IProgressProvider progressProvider, GameField gameField, GameObject slotsControl,
            IAutoProcessesController autoProcessesController)
        {
            _progressProvider = progressProvider;
            _gameField = gameField;
            _slotsControl = slotsControl;
            _autoProcessesController = autoProcessesController;
        }

        public void CreateAnvil()
        {
            AnvilDisplay display = _slotsControl.GetComponentInChildren<AnvilDisplay>();
            display.Construct(_gameField);
            _anvilInstance = display.AnvilInstance;
            
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