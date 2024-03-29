using System;

namespace Sources.ItemBase
{
    /// <summary>
    /// An item, can be stored in slots
    /// </summary>
    public class Item
    {
        private const int MinItemLevel = 1;
        private int _level;
        public event Action LevelIncreased;

        public Item(int itemLevel)
        {
            if (itemLevel <= 0)
                itemLevel = MinItemLevel;

            _level = itemLevel;
        }

        /// <summary>
        /// Upgrades an item
        /// </summary>
        public void Upgrade() =>
            IncreaseLevel();

        /// <summary>
        /// Increases item level by one
        /// </summary>
        private void IncreaseLevel()
        {
            _level++;
            LevelIncreased?.Invoke();
        }

        public int Level => _level;
    }
}