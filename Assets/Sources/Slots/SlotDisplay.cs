using UnityEngine;
using UnityEngine.UI;

namespace Sources.Slots
{
    // representing slot, created for dividing model and view layers 
    public class SlotDisplay : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField, Tooltip("Slot's background")] private Image _frame;
        
        private Slot _slot;

        public void Construct()
        {
            _slot = new Slot(this);
            _slot.SlotConditionUpdated += UpdateView;
        }

        private void OnDisable()
        {
            _slot.SlotConditionUpdated -= UpdateView;
        }

        // Once slot is changing its condition, view will be updated afterwards
        private void UpdateView()
        {
            if(_slot.IsLocked)
                _frame.color = Color.black; // simplified for not focusing on graphical part, but to highlight that the slot is locked
            if(!_slot.IsEmpty)
                _frame.color = Color.blue;
        }

        public Slot Slot => _slot;
    }
}