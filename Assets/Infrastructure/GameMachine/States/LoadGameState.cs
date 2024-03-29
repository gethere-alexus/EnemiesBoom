using Infrastructure.Curtain;
using Infrastructure.SceneLoad;
using Infrastructure.Services.Factories.UIFactory;

namespace Infrastructure.GameMachine.States
{
    /// <summary>
    /// The state where all the game components are being instantiated.
    /// </summary>
    public class LoadGameState : IState 
    {
        private const int GameSceneIndex = 1;
        
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _gameStateMachine;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IUIFactory _uiFactory;

        public LoadGameState(SceneLoader sceneLoader, GameStateMachine gameStateMachine, LoadingCurtain loadingCurtain,
            IUIFactory uiFactory)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            _loadingCurtain = loadingCurtain;
            _uiFactory = uiFactory;
        }

        public void Enter() => 
            _sceneLoader.Load(sceneIndex: GameSceneIndex, OnLoaded);

        /// <summary>
        /// Building the components of the game scene here.
        /// </summary>
        private void OnLoaded()
        {
            _loadingCurtain.ShowCurtain();
            
            _uiFactory.CreateUIRoot();
            _uiFactory.CreateSlots();
            _uiFactory.CreateSlotsControl();
            
            _gameStateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
           _loadingCurtain.HideCurtain();
        }
    }
}