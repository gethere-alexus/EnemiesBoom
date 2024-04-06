using UnityEngine;

namespace Sources.HeroBase.Storing
{
    public class HeroSlotDisplay : MonoBehaviour
    {
        private HeroSlot _slotInstance;

        public void Construct(HeroSlot presenter)
        {
            _slotInstance = presenter;
        }
    }
}