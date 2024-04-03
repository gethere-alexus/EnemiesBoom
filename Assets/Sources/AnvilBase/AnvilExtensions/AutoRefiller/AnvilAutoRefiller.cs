using System.Collections;
using Infrastructure.ProgressData;
using Infrastructure.Services.AutoProcessesControll.Connection;
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

        public void Construct(Anvil anvil)
        {
            _anvil = anvil;
            anvil.ItemCrafted += RestartProcess;
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
                yield return new WaitForSeconds(_refillCoolDown);

                if (!_anvil.IsCompletelyCharged)
                    _anvil.AddCharge(_amountChargesToAdd);
            }
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            _anvil.ItemCrafted -= RestartProcess;
        }
    }
}