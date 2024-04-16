using Sources.Item.ItemSlotBase;
using TMPro;
using UnityEngine;

namespace Sources.Item.ItemBase
{
    /// <summary>
    /// Item view model
    /// </summary>
    [RequireComponent(typeof(ItemInput))]
    public class ItemDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private ItemInput _itemInput;
        
        private ItemSlotDisplay _storingItemSlot;
        private Item _itemInstance;
        
        private void Awake() =>
            _itemInput ??= GetComponent<ItemInput>();

        public void Construct(Item displayingItem, ItemSlotDisplay storedAt, ItemDrag itemDrag)
        {
            _storingItemSlot = storedAt;
            _itemInstance = displayingItem;
            
            _itemInstance.LevelChanged += UpdateDisplay;
            _itemInput.ItemMerging += storedAt.ItemSlotInstance.OnMerging;
            
            itemDrag.SubscribeInput(_itemInput);
            UpdateDisplay();
        }
        
        private void UpdateDisplay() => 
            _levelText.text = _itemInstance.Level.ToString();

        private void OnDisable()
        {
            _itemInstance.LevelChanged -= UpdateDisplay;
            _itemInput.ItemMerging += _storingItemSlot.ItemSlotInstance.OnMerging;
        }
        

        public Item ItemInstance => _itemInstance;
        public ItemSlotDisplay StoredAt => _storingItemSlot;
    }
}