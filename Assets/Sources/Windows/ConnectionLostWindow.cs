namespace Sources.Windows
{
    public class ConnectionLostWindow : WindowBase
    {
        private void Awake() => 
            DontDestroyOnLoad(gameObject);

        public override void Close() =>
            Destroy(gameObject);
    }
}