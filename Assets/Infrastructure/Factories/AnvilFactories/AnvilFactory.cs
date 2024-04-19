using Infrastructure.Factories.ItemFactory;
using Infrastructure.Services.AutoPlayControl;
using Infrastructure.Services.ProgressLoad;
using Sources.AnvilBase;
using Sources.AnvilBase.AnvilExtensions.AutoRefiller;
using Sources.AnvilBase.AnvilExtensions.AutoUse;
using Sources.AnvilBase.AnvilExtensions.ChargesRefiller;
using Sources.ItemsBase.ItemFieldBase;
using Zenject;

namespace Infrastructure.Factories.AnvilFactories
{
    public class AnvilFactory : IAnvilFactory
    {
        private readonly IItemFieldFactory _fieldFactory;
        private readonly IProgressProvider _progressProvider;
        private readonly IAutoPlayController _autoPlayController;
        private readonly DiContainer _diRegistry;

        private Anvil _anvilInstance;

        public AnvilFactory(IItemFieldFactory fieldFactory, IProgressProvider progressProvider, IAutoPlayController autoPlayController,
            DiContainer diRegistry, ItemField itemField)
        {
            _fieldFactory = fieldFactory;
            _progressProvider = progressProvider;
            _autoPlayController = autoPlayController;
            _diRegistry = diRegistry;

            _anvilInstance = new Anvil(itemField);
            diRegistry.Bind<Anvil>().FromInstance(_anvilInstance).AsSingle();
        }

        public void CreateAnvil()
        {
            AnvilDisplay display = _fieldFactory.FieldControl.AnvilDisplay;
            display.Construct(_anvilInstance);

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
            AnvilAutoUse autoUsing = _fieldFactory.FieldControl.AnvilAutoUseInstance;
            autoUsing.Construct(_anvilInstance);

            _progressProvider.RegisterObserver(autoUsing);
            _autoPlayController.RegisterAutoPlay(autoUsing);
            
            _diRegistry.Bind<AnvilAutoUse>().FromInstance(autoUsing).AsSingle();
        }

        private void ConstructAnvilAutoRefiller()
        {
            AnvilAutoRefiller autoRefiller = _fieldFactory.FieldControl.AnvilAutoRefiller;
            autoRefiller.Construct(_anvilInstance);

            _progressProvider.RegisterObserver(autoRefiller);
            _autoPlayController.RegisterAutoPlay(autoRefiller);
            _diRegistry.Bind<AnvilAutoRefiller>().FromInstance(autoRefiller).AsSingle();
        }

        private void ConstructAnvilRefiller()
        {
            AnvilChargesRefillDisplay refillDisplay = _fieldFactory.FieldControl.AnvilChargesRefillDisplay;
            refillDisplay.Construct(_anvilInstance);

            _progressProvider.RegisterObserver(refillDisplay.ChargesRefillInstance);
        }
        public Anvil Anvil => _anvilInstance;
    }
}