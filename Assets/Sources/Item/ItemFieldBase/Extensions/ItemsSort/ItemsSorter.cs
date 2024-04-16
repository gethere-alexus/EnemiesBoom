using Sources.Item.ItemSlotBase;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Item.ItemFieldBase.Extensions.ItemsSort
{
    /// <summary>
    /// Sorts the grid with certain 
    /// </summary>
    public class ItemsSorter : MonoBehaviour
    {
        [SerializeField] private Button _sortButton;
        public delegate bool Comparer(ItemBase.Item a, ItemBase.Item b);

        private Comparer _sortingStyle;
        private ItemField _itemField;

        public void Construct(ItemField itemField, Comparer sortingStyle)
        {
            _sortingStyle = sortingStyle;
            _itemField = itemField;
        }

        private void SortSlots()
        {
            ItemSlot[] grid = _itemField.Grid;
            Sort(grid, _sortingStyle);
        }

        /// <summary>
        /// Sorts grid using Bubble sort algorithms, from the max level to the lowest one
        /// Algorithm : https://www.geeksforgeeks.org/bubble-sort/
        /// </summary>
        private static void Sort(ItemSlot[] grid, Comparer comparer)
        {
            for (int i = 0; i < grid.Length - 1; i++)
            {
                bool isSwapped = false;
                for (int j = 0; j < grid.Length - i - 1; j++)
                {
                    ItemSlot firstItemSlot = grid[j], secondItemSlot = grid[j + 1];

                    bool isItemAbsent = firstItemSlot.StoringItem == null;
                    if (isItemAbsent || comparer(firstItemSlot.StoringItem, secondItemSlot.StoringItem))
                    {
                        var temp = grid[j].StoringItem;

                        firstItemSlot.ReplaceItem(grid[j + 1].StoringItem);
                        secondItemSlot.ReplaceItem(temp);

                        isSwapped = true;
                    }

                    if (grid[j + 1].IsLocked)
                        break;
                }

                if (isSwapped == false)
                    break;
            }
        }

        private void OnEnable()
        {
            _sortButton.onClick.AddListener(SortSlots);
        }

        private void OnDisable()
        {
            _sortButton.onClick.RemoveAllListeners();
        }
    }
}