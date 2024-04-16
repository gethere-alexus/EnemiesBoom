using System;

namespace Sources.Wallet
{
    public interface IWallet
    {
        event Action BalanceChanged;
        void AddMoney(int amountToAdd);
        void TakeMoney(int toTake, out bool isSucceeded);
        
        int Balance { get; }
    }
}