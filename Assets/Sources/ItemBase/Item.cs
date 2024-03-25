using System;

namespace Sources.ItemBase
{
    public class Item
    {
        private int _level;
        public event Action LevelIncreased;

        public Item(int arrowLevel)
        {
            _level = arrowLevel;
        }

        public void TryMerge(Item itemToPlace, out bool isSucceeded)
        {
            isSucceeded = false;
            if (itemToPlace.Level == Level)
            {
                isSucceeded = true;
                IncreaseItemLevel();
            }
        }

        private void IncreaseItemLevel()
        {
            _level++;
            LevelIncreased?.Invoke();
        }

        public int Level => _level;
    }
}