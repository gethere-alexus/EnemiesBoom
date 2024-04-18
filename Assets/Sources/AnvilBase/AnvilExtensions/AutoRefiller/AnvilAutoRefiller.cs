using System.Collections;
using Infrastructure.ProgressData;
using Infrastructure.Services.AutoProcessesControl.Connection;
using Infrastructure.Services.ProgressLoad;
using Infrastructure.Services.ProgressLoad.Connection;
using UnityEngine;

namespace Sources.AnvilBase.AnvilExtensions.AutoRefiller
{
    /// <summary>
    /// Automatically refills anvil's charges if it is not full.
    /// Parametrized by AnvilAutoRefillConfig.
    /// </summary>
    public class AnvilAutoRefiller : MonoBehaviour, IProgressWriter, IAutoProcessController
    {
        private Anvil _anvil;
        private int _amountChargesToAdd;
        private float _refillCoolDown;
        private float _currentCooldownPercent;
        private const float MinRefillCoolDown = 0.1f;

        public void Construct(Anvil anvil)
        {
            _anvil = anvil;
            anvil.ItemCrafted += RestartProcess;
        }

        public void DecreaseRefillCoolDown(float decreaseBy)
        {
            StopProcess();
            
            _refillCoolDown -= decreaseBy;
            
            if (_refillCoolDown < MinRefillCoolDown)
                _refillCoolDown = MinRefillCoolDown;
            
            StartProcess();
        }

        public void StartProcess()
        {
            StartCoroutine(StartAutoRefilling());
        }

        public void RestartProcess()
        {
            StopAllCoroutines();
            StartCoroutine(StartAutoRefilling());
        }

        public void StopProcess()
        {
            StopAllCoroutines();
        }

        public void LoadProgress(GameProgress progress)
        {
            _amountChargesToAdd = progress.AnvilExtensions.AnvilAutoRefiller.AmountChargesToAdd;
            _refillCoolDown = progress.AnvilExtensions.AnvilAutoRefiller.RefillCoolDown;
        }

        public void SaveProgress(GameProgress progress)
        {
            progress.AnvilExtensions.AnvilAutoRefiller.AmountChargesToAdd = _amountChargesToAdd;
            progress.AnvilExtensions.AnvilAutoRefiller.RefillCoolDown = _refillCoolDown;
        }

        private IEnumerator StartAutoRefilling()
        {
            while (true)
            {
                float pastTime = 0;
                
                while (pastTime <= _refillCoolDown)
                {
                    pastTime += Time.deltaTime;
                    float percentElapsed = (pastTime / _refillCoolDown);
                    _currentCooldownPercent = percentElapsed;
                    yield return null;
                }
                
                if (!_anvil.IsCompletelyCharged)
                    _anvil.AddCharge(_amountChargesToAdd);
            }
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            _anvil.ItemCrafted -= RestartProcess;
        }

        public float CooldownPercent => _currentCooldownPercent;
    }
}