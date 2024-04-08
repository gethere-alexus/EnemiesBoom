namespace Sources.Windows
{
    public class ConnectionLostWindow : WindowBase
    {
        protected override void OnAwake() => 
            DontDestroyOnLoad(gameObject);

        protected override void OnDestroying()
        {
            
        }

        public override void Close() =>
            Destroy(gameObject);
    }
}