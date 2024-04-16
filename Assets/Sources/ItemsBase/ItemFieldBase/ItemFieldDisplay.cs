using Sources.ItemsBase.ItemBase;
using UnityEngine;

namespace Sources.ItemsBase.ItemFieldBase
{
    /// <summary>
    /// View model of game field, the main purpose is to build a grid and initialize the model.
    /// </summary>
    public class ItemFieldDisplay : MonoBehaviour
    {
        [SerializeField, Tooltip("Where all the slots will be stored")]
        private Transform _slotStorage;

        [SerializeField, Tooltip("Transform for an item when it is being dragged")]
        private ItemDrag _itemDrag;

        private ItemField _itemFieldInstance;
        private const int GridSlots = 40;

        public void Construct()
        {
            ClearStorage();
            _itemFieldInstance = new ItemField();
        }

        /// <summary>
        /// Clears grid storage if there is some game objects.
        /// </summary>
        private void ClearStorage()
        {
            // Clears the slots storage if there is any existing slots
            if (_slotStorage.childCount != 0)
            {
                foreach (Transform child in _slotStorage)
                    Destroy(child.gameObject);
            }
        }

        public ItemField ItemFieldInstance => _itemFieldInstance;

        public ItemDrag ItemDrag => _itemDrag;

        public Transform SlotsStorage => _slotStorage;
    }
}