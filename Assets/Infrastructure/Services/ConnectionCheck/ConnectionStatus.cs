namespace Infrastructure.Services.ConnectionCheck
{
    public enum ConnectionStatus
    {
        Connected = 0,
        ConnectedWithoutInternet = 1,
        Disconnected = 2,
    }
}