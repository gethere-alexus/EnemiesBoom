using Infrastructure.Curtain;
using Infrastructure.GameMachine.States;
using Infrastructure.SceneLoad;
using UnityEngine;
using Zenject;

namespace Infrastructure.Bootstrap
{
    /// <summary>
    /// Game entry point, called in bootstrap scene.
    /// </summary>
    public class EntryPoint : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;
        private Game _gameInstance;

        private void Awake()
        {
            DiContainer container = FindObjectOfType<ProjectContext>().Container;
            
            _gameInstance = new Game(this, container, Instantiate(_loadingCurtain));
            _gameInstance.GameStateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }

        public Game GameInstance => _gameInstance;
    }
}