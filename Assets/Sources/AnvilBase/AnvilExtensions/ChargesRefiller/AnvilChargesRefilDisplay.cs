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
        
        public void Construct(Anvil anvil)
        {
            _chargesRefillInstance = new AnvilChargesRefill(anvil);
            UpdateDisplay();
            
            _chargesRefillInstance.RefillChargesUpdated += UpdateDisplay;
            _refillButton.onClick.AddListener(_chargesRefillInstance.RefillAnvil);
        }

        private void OnDisable()
        {
            _refillButton.onClick.RemoveAllListeners();
            _chargesRefillInstance.RefillChargesUpdated -= UpdateDisplay;
        }

        private void UpdateDisplay()
        {
            _refillInformation.text = $"Left charges : {_chargesRefillInstance.Charges}";
        }

        public AnvilChargesRefill ChargesRefillInstance => _chargesRefillInstance;
    }
}