using Sources.Infrastructure.Curtain;
using Sources.Infrastructure.GameMachine.States;
using Sources.Infrastructure.SceneLoad;
using UnityEngine;
using Zenject;

namespace Sources.Infrastructure.Bootstrap
{
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
    }
}