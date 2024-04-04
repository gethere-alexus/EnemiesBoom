using Infrastructure.Bootstrap;

namespace Infrastructure.Services.WindowProvider
{
    public class ConnectionLostWindow : WindowBase
    {
        private void Awake() => 
            DontDestroyOnLoad(gameObject);

        public override void Close() =>
            Destroy(gameObject);
    }
}