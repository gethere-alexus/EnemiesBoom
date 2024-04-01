using Infrastructure.Configurations.Anvil;
using Infrastructure.ProgressData.Anvil;
using Infrastructure.Services.ProgressProvider;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.AnvilBase.AnvilExtensions.ChargesRefiller
{
    public class AnvilChargesRefillDisplay : MonoBehaviour
    {
        [SerializeField] private Button _refillButton;
        [SerializeField] private TMP_Text _refillInformation;

        private AnvilChargesRefill _chargesRefillInstance;

        /// <summary>
        /// Constructs from config
        /// </summary>
        public void Construct(Anvil anvil, IProgressProvider progressProvider, AnvilRefillConfig refillConfig)
        {
            _chargesRefillInstance = new AnvilChargesRefill(anvil, progressProvider, refillConfig);
            UpdateDisplay();
            
            _chargesRefillInstance.RefillChargeSpent += UpdateDisplay;
            _refillButton.onClick.AddListener(_chargesRefillInstance.RefillAnvil);
        }

        /// <summary>
        /// Constructs from save
        /// </summary>
        public void Construct(Anvil anvil, IProgressProvider progressProvider, AnvilRefillData save)
        {
            _chargesRefillInstance = new AnvilChargesRefill(anvil, progressProvider, save);
            UpdateDisplay();
            
            _chargesRefillInstance.RefillChargeSpent += UpdateDisplay;
            _refillButton.onClick.AddListener(_chargesRefillInstance.RefillAnvil);
        }

        private void OnDisable()
        {
            _refillButton.onClick.RemoveAllListeners();
            _chargesRefillInstance.RefillChargeSpent -= UpdateDisplay;
        }

        private void UpdateDisplay()
        {
            _refillInformation.text = $"Left charges : {_chargesRefillInstance.Charges}";
        }

        public AnvilChargesRefill ChargesRefillInstance => _chargesRefillInstance;
    }
}