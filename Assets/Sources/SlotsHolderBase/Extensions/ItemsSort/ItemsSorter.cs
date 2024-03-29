using Sources.ItemBase;
using Sources.SlotBase;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.SlotsHolderBase.Extensions.ItemsSort
{
    /// <summary>
    /// Sorts the grid with certain 
    /// </summary>
    public class ItemsSorter : MonoBehaviour
    {
        [SerializeField] private Button _sortButton;
        public delegate bool Comparer(Item a, Item b);

        private Comparer _sortingStyle;
        private SlotsHolder _slotsHolder;

        public void Construct(SlotsHolder slotsHolder, Comparer sortingStyle)
        {
            _sortingStyle = sortingStyle;
            _slotsHolder = slotsHolder;
        }

        private void SortSlots()
        {
            Slot[] grid = _slotsHolder.Grid;
            Sort(grid, _sortingStyle);
        }

        /// <summary>
        /// Sorts grid using Bubble sort algorithms, from the max level to the lowest one
        /// Algorithm : https://www.geeksforgeeks.org/bubble-sort/
        /// </summary>
        private static void Sort(Slot[] grid, Comparer comparer)
        {
            for (int i = 0; i < grid.Length - 1; i++)
            {
                bool isSwapped = false;
                for (int j = 0; j < grid.Length - i - 1; j++)
                {
                    Slot firstSlot = grid[j], secondSlot = grid[j + 1];

                    bool isItemAbsent = firstSlot.StoringItem == null;
                    if (isItemAbsent || comparer(firstSlot.StoringItem, secondSlot.StoringItem))
                    {
                        var temp = grid[j].StoringItem;

                        firstSlot.ReplaceItem(grid[j + 1].StoringItem);
                        secondSlot.ReplaceItem(temp);

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