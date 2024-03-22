using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Infrastructure.Curtain
{
    [RequireComponent(typeof(CanvasGroup))]
    public class LoadingCurtain : MonoBehaviour
    {
        [Header("Set up")]
        [SerializeField] private CanvasGroup _curtain;
        [SerializeField] private Slider _progressSlider;
        [SerializeField] private TMP_Text _progressText;
        
        [Header("Configuration")]
        [SerializeField] private float _timeToHideCurtain;
        
        private const float InitialAlpha = 1.0f;

        public void SetProgress(float sceneAsyncProgress)
        {
            _progressSlider.value = sceneAsyncProgress;
            _progressText.text = $"{sceneAsyncProgress * 100.0f}%";
        }

        public void ShowCurtain()
        {
           gameObject.SetActive(true);
           _curtain.alpha = InitialAlpha;
        }

        public void HideCurtain()
        {
            SetProgress(1.0f);
            StartCoroutine(HideSmoothly(_timeToHideCurtain));
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private IEnumerator HideSmoothly(float timeToHide)
        {
            float pastTime = 0;
            float currentAlpha = InitialAlpha;
            
            while (pastTime <= _timeToHideCurtain)
            {
                pastTime += Time.deltaTime;
                currentAlpha = Mathf.Lerp(currentAlpha, 0, pastTime);
                _curtain.alpha = currentAlpha;
                yield return null;
            }

            gameObject.SetActive(false);
        }
    }
}