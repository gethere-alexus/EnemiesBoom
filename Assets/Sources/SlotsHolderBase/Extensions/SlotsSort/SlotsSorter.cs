using Sources.SlotBase;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.SlotsHolderBase.Extensions.SlotsSort
{
    /// <summary>
    /// Sorts the grid
    /// </summary>
    public class SlotsSorter : MonoBehaviour
    {
        [SerializeField] private Button _sortButton;

        private SlotsHolder _slotsHolder;

        public void Construct(SlotsHolder slotsHolder)
        {
            _slotsHolder = slotsHolder;
        }

        /// <summary>
        /// Sorts grid using Bubble sort algorithms, from the max level to the lowest one
        /// Algorithm : https://www.geeksforgeeks.org/bubble-sort/
        /// </summary>
        private void SortSlots()
        {
            Slot[] grid = _slotsHolder.Grid;
            for (int i = 0; i < grid.Length - 1; i++)
            {
                bool isSwapped = false;
                for (int j = 0; j < grid.Length - i - 1; j++)
                {
                    bool isItemAbsent = grid[j].StoringItem == null;
                    bool isNextLevelSmaller = grid[j].StoringItem?.Level < grid[j + 1].StoringItem?.Level;

                    if (isItemAbsent || isNextLevelSmaller)
                    {
                        var temp = grid[j].StoringItem;
                        
                        grid[j].RemoveStoringItem();
                        grid[j].PutItem(grid[j + 1].StoringItem);
                        
                        grid[j + 1].RemoveStoringItem();
                        grid[j + 1].PutItem(temp);

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