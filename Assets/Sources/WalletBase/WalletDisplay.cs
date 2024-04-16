using TMPro;
using UnityEngine;

namespace Sources.WalletBase
{
    public class WalletDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _balanceText;
        private IWallet _walletInstance;

        public void Construct(IWallet instance)
        {
            _walletInstance = instance;
            _walletInstance.BalanceChanged += UpdateView;
            UpdateView();
        }

        private void UpdateView() => 
            _balanceText.text = WalletUtility.ConvertToPrettyFormat(_walletInstance.Balance, '.');

        private void OnDisable() => 
            _walletInstance.BalanceChanged -= UpdateView;
    }
}