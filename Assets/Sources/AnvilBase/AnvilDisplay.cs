using Infrastructure.Configurations.Anvil;
using Infrastructure.ProgressData.AnvilData;
using Infrastructure.Services.ProgressProvider;
using Sources.SlotsHolderBase;
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

        public void Construct(SlotsHolder slotsHolder, IProgressProvider progressProvider, AnvilConfig anvilConfig)
        {
            _anvilInstance = new Anvil(slotsHolder, progressProvider, anvilConfig);
            AssignListeners();
        }

        public void Construct(SlotsHolder slotsHolder, IProgressProvider progressProvider, AnvilProgress progress)
        {
            _anvilInstance = new Anvil(slotsHolder, progressProvider, progress);
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