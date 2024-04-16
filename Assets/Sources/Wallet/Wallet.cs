using System;

namespace Sources.Wallet
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

        public int Balance => _balance;
    }
}