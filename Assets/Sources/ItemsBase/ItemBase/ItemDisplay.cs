using TMPro;
using UnityEngine;

namespace Sources.ItemsBase.ItemBase
{
    /// <summary>
    /// Item view model
    /// </summary>
    public class ItemDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private ItemInput _itemInput;
        
        private IItemStorage _storingItemSlot;
        private Item _itemInstance;
        
        private void Awake() =>
            _itemInput ??= GetComponent<ItemInput>();

        public void Construct(Item displayingItem, IItemStorage storedAt, ItemDrag itemDrag)
        {
            _storingItemSlot = storedAt;
            _itemInstance = displayingItem;
            
            _itemInstance.LevelChanged += UpdateDisplay;
            
            itemDrag.SubscribeInput(_itemInput);
            UpdateDisplay();
        }
        
        private void UpdateDisplay() => 
            _levelText.text = _itemInstance.Level.ToString();

        private void OnDisable()
        {
            _itemInstance.LevelChanged -= UpdateDisplay;
        }
        public Item ItemInstance => _itemInstance;
        public IItemStorage StoredAt => _storingItemSlot;
    }
}