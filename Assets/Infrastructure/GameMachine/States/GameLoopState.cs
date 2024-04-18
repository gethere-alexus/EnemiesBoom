using System.Collections;
using Infrastructure.SceneLoad;
using Infrastructure.Services.AutoProcessesControl;
using Infrastructure.Services.ConfigLoad;
using Infrastructure.Services.ProgressLoad;
using UnityEngine;

namespace Infrastructure.GameMachine.States
{
    /// <summary>
    /// The state starts once components are instantiated and data loaded.
    /// </summary>
    public class GameLoopState : IState 
    {
        private readonly IProgressProvider _progressProvider;
        private readonly IAutoProcessesController _autoProcessesController;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IConfigProvider _configProvider;

        private const float SaveDelay = 5.0f; 
        public GameLoopState(IProgressProvider progressProvider, IAutoProcessesController autoProcessesController,
            ICoroutineRunner coroutineRunner, IConfigProvider configProvider)
        {
            _progressProvider = progressProvider;
            _autoProcessesController = autoProcessesController;
            _coroutineRunner = coroutineRunner;
            _configProvider = configProvider;
        }

        public void Enter()
        {
            _coroutineRunner.StartCoroutine(StartAutoSaving());
            _autoProcessesController.StartAllProcesses();
        }
        private IEnumerator StartAutoSaving()
        {
            while (true)
            {
                _progressProvider.SaveProgress();
                yield return new WaitForSeconds(SaveDelay);
            }
        }

        public void Exit()
        {
            _coroutineRunner.StopCoroutine(StartAutoSaving());
            _autoProcessesController.StopAllProcesses();
            _progressProvider.SaveProgress();
            
            _progressProvider.ClearObservers();
            _autoProcessesController.ClearControllers();
            _configProvider.ClearLoaders();
        }
    }
}