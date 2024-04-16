using System;
using Infrastructure.Services.ProgressLoad.Connection;

namespace Sources.WalletBase
{
    public interface IWallet : IProgressWriter
    {
        event Action BalanceChanged;
        void AddMoney(int amountToAdd);
        void TakeMoney(int toTake, out bool isSucceeded);
        
        int Balance { get; }
    }
}