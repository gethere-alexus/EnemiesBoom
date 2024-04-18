using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.AnvilBase.AnvilExtensions.AutoRefiller
{
    public class AnvilRefillAnimation : MonoBehaviour
    {
        [SerializeField] private AnvilAutoRefiller _anvilAutoRefiller;
        [SerializeField] private Slider _progressBar;

        private const float CompletelyCharged = 1.0f;
        private const float NotCharged = 0.0f;
        private void Awake()
        {
            _progressBar.minValue = NotCharged;
            _progressBar.maxValue = CompletelyCharged;

            _progressBar.value = NotCharged;
        }
        
        private void OnEnable() => 
            StartCoroutine(AnimateCooldown());

        private IEnumerator AnimateCooldown()
        {
            while (true)
            {
                yield return null;
                _progressBar.value = _anvilAutoRefiller.CooldownPercent;
            }
        }

        private void OnDisable() => 
            StopAllCoroutines();
    }
}