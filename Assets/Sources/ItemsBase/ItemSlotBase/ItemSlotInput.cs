using Sources.ItemsBase.ItemBase;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sources.ItemsBase.ItemSlotBase
{
    [RequireComponent(typeof(ItemSlotDisplay))]
    public class ItemSlotInput : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private ItemSlotDisplay _controllingItemSlot;
        private const int DoubleClickCount = 2;
        private float _lastTimeClick;

        private void Awake() =>
            _controllingItemSlot ??= GetComponent<ItemSlotDisplay>();

        public void OnPointerClick(PointerEventData eventData)
        {
            float currentTimeClick = eventData.clickTime;
            if(Mathf.Abs(currentTimeClick - _lastTimeClick) < 0.75f)
            {
                _controllingItemSlot.ItemSlotInstance.OnMerging();
            }
            _lastTimeClick = currentTimeClick;
        }
    }
}