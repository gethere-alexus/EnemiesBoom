using System.Collections;
using Infrastructure.Configurations.Anvil;
using UnityEngine;

namespace Sources.AnvilBase.AnvilExtensions.AutoUse
{
    /// <summary>
    /// Automatically uses the anvil, creating the items
    /// Parametrized by AnvilAutoUseConfig.
    /// </summary>
    public class AnvilAutoUse : MonoBehaviour
    {
        private float _usingCooldown;
        private Anvil _anvil;
        
        /// <param name="anvil">Applying anvil</param>
        /// <param name="autoUseConfig">configuration</param>
        public void Construct(Anvil anvil, AnvilAutoUseConfig autoUseConfig)
        {
            _anvil = anvil;
            _usingCooldown = autoUseConfig.UsingCooldown;
            _anvil.ItemCrafted += RestartAutoCreation;

            StartCoroutine(CreateItemsAutomatically());
        }

        /// <summary>
        /// Restarts auto-creation coroutine
        /// </summary>
        private void RestartAutoCreation()
        {
            StopAllCoroutines();
            StartCoroutine(CreateItemsAutomatically());
        }


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
            _anvil.ItemCrafted -= RestartAutoCreation;
            StopAllCoroutines();
        }
    }
}