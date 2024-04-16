namespace Sources.Windows.ConnectionLost
{
    public class ConnectionLostWindow : WindowBase
    {
        protected override void OnAwake()
        {
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }

        protected override void Close()
        {
            base.Close();
            Destroy(gameObject);
        }
    }
}