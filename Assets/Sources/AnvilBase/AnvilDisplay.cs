using Sources.GameFieldBase;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.AnvilBase
{
    public class AnvilDisplay : MonoBehaviour
    {
        [SerializeField] private Button _anvilButton;
        [SerializeField] private TMP_Text _chargesInformation;

        private Anvil _anvilInstance;

        public void Construct(GameField gameField)
        {
            _anvilInstance = new Anvil(gameField);
            AssignListeners();
        }

        private void AssignListeners()
        {
            _anvilButton.onClick.AddListener(_anvilInstance.CraftItem);
            _anvilInstance.ChargesUpdated += UpdateView;

            UpdateView();
        }

        private void UpdateView() =>
            _chargesInformation.text = $"{_anvilInstance.ChargesLeft}/{_anvilInstance.MaxCharges}";

        private void OnDisable()
        {
            _anvilInstance.ChargesUpdated -= UpdateView;
            _anvilButton.onClick.RemoveAllListeners();
        }

        public Anvil AnvilInstance => _anvilInstance;
    }
}