using System.Collections;
using Infrastructure.ProgressData;
using Infrastructure.Services.AutoProcessesControll.Connection;
using Infrastructure.Services.ProgressLoad;
using Infrastructure.Services.ProgressLoad.Connection;
using UnityEngine;

namespace Sources.AnvilBase.AnvilExtensions.AutoUse
{
    /// <summary>
    /// Automatically uses the anvil, creating the items
    /// Parametrized by AnvilAutoUseConfig.
    /// </summary>
    public class AnvilAutoUse : MonoBehaviour, IAutoProcessController, IProgressWriter
    {
        private Anvil _anvil;
        
        private float _usingCooldown;

        public void Construct(Anvil anvil)
        {
            _anvil = anvil;
            _anvil.ItemCrafted += RestartProcess;
            
        }

        public void StartProcess() =>
            StartCoroutine(CreateItemsAutomatically());

        public void RestartProcess()
        {
            StopAllCoroutines();
            StartCoroutine(CreateItemsAutomatically());
        }

        public void StopProcess() =>
            StopAllCoroutines();

        public void LoadProgress(GameProgress progress) => 
            _usingCooldown = progress.AnvilExtensions.AnvilAutoUse.UsingCoolDown;

        public void SaveProgress(GameProgress progress) => 
            progress.AnvilExtensions.AnvilAutoUse.UsingCoolDown = _usingCooldown;


        /// <summary>
        /// Looped method which crafts arrow with parametrized cooldown. 
        /// </summary>
        private IEnumerator CreateItemsAutomatically()
        {
            while (true)
            {
                yield return new WaitForSeconds(_usingCooldown);
                _anvil.CraftItem();
            }
        }

        private void OnDisable()
        {
            _anvil.ItemCrafted -= RestartProcess;
            StopAllCoroutines();
        }
    }
}