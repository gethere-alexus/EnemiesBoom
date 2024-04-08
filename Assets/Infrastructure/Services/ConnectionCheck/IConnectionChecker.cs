using System;

namespace Infrastructure.Services.ConnectionCheck
{
    /// <summary>
    /// Checks if player has internet connection
    /// </summary>
    public interface IConnectionChecker : IService
    {
        ConnectionStatus NetworkStatus { get; }
        bool IsNetworkConnected { get; }
        event Action ConnectionLost, ConnectionRenewed;
    }
}