using System.Collections;
using Infrastructure.Configurations.Anvil;
using UnityEngine;

namespace Sources.AnvilBase.AnvilExtensions
{
    public class AnvilAutoRefiller : MonoBehaviour
    {
        private Anvil _anvil;
        private int _amountChargesToAdd;
        private float _refillCoolDown;


        public void Construct(Anvil anvil, AnvilAutoRefillConfig autoRefillerConfig)
        {
            _anvil = anvil;
            _refillCoolDown = autoRefillerConfig.RefillCoolDown;
            _amountChargesToAdd = autoRefillerConfig.AmountChargesToAdd;
            
            StartCoroutine(StartAutoRefilling());
        }

        private IEnumerator StartAutoRefilling()
        {
            while (true)
            {
                yield return new WaitForSeconds(_refillCoolDown);
                
                if(!_anvil.IsCompletelyCharged)
                    _anvil.AddCharge(_amountChargesToAdd);
                Debug.Log("AutoRefilled");
            }
        }

        private void OnDisable() => 
            StopAllCoroutines();
    }
}