using System;
using Sources.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Slots
{
    [RequireComponent(typeof(Button))]
    public class Anvil : MonoBehaviour
    {
        [Header("Set Up")] 
        [SerializeField] private SlotsHolder _slotsHolder;
        [SerializeField] private Button _anvilButton;
        
        [Header("Configuration")]
        [SerializeField, Tooltip("Maximum amount of times player can craft arrow without CD")] private int _maxUsages;
        [SerializeField, Tooltip("Level of arrow that will be crafted")] private int _craftingArrowLevel;
        [SerializeField] private int _usagesLeft; // serialized for testing purposes

        private void Awake()
        {
            _usagesLeft = _maxUsages;
        }

        public void CraftArrow()
        {
            if (_usagesLeft > 0)
            {
                _slotsHolder.PlaceArrow(new Arrow(_craftingArrowLevel), out bool isSucceeded);
                if (isSucceeded)
                    SpendUsage();
            }
        }

        private void SpendUsage() => 
            _usagesLeft--;

        private void OnEnable()
        {
            _anvilButton.onClick.AddListener(CraftArrow);
        }

        private void OnDisable()
        {
            _anvilButton.onClick.RemoveAllListeners();
        }
    }
}