using UnityEngine;
using UnityEngine.UI;


namespace Sources.Windows
{
    public class HeroesInventoryWindow : WindowBase
    {
        [SerializeField] private Button _closeButton;
        
        protected override void OnAwake() => 
            _closeButton.onClick.AddListener(Close);

        protected override void OnDestroying() => 
            _closeButton.onClick.RemoveAllListeners();

        public override void Close() => 
            Destroy(gameObject);
    }
}