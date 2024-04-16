using System;
using Infrastructure.ProgressData;
using Infrastructure.Services.ProgressLoad.Connection;

namespace Sources.WalletBase
{
    public class Wallet : IWallet
    {
        private int _balance = Empty;
        private const int Empty = 0;

        public event Action BalanceChanged;

        public void AddMoney(int amount)
        {
            _balance += amount;
            BalanceChanged?.Invoke();
        }

        public void TakeMoney(int toTake, out bool isSucceeded)
        {
            isSucceeded = false;
            if (_balance - toTake >= Empty)
            {
                _balance -= toTake;
                isSucceeded = true;
                BalanceChanged?.Invoke();
            }
        }

        public void LoadProgress(GameProgress progress)
        {
            _balance = progress.WalletData.Balance;
            BalanceChanged?.Invoke();
        }

        public void SaveProgress(GameProgress progress)
        {
            progress.WalletData.Balance = _balance;
            BalanceChanged?.Invoke();
        }


        public int Balance => _balance;
    }
}