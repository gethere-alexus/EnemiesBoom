using System;
using System.Collections;
using Sources.Infrastructure.Curtain;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.Infrastructure.SceneLoad
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly LoadingCurtain _curtain;

        public SceneLoader(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            _coroutineRunner = coroutineRunner;
            _curtain = curtain;
        }

        public void Load(int sceneIndex, Action sceneLoaded = null) =>
            _coroutineRunner.StartCoroutine(LoadScene(sceneIndex, sceneLoaded));

        private IEnumerator LoadScene(int sceneIndex, Action sceneLoaded = null)
        {
            if (sceneIndex == SceneManager.GetActiveScene().buildIndex)
            {
                sceneLoaded?.Invoke();
                yield break;
            }
            
            AsyncOperation sceneAsync = SceneManager.LoadSceneAsync(sceneIndex);

            while (!sceneAsync.isDone)
            {
                _curtain.SetProgress(sceneAsync.progress);
                yield return null;
            }

            sceneLoaded?.Invoke();
        }
    }
}