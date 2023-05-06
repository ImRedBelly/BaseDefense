using System;

namespace Library.Data
{
    public class CurrencyDataModel
    {
        public event Action<int> OnAppendCurrency;
        public event Action<int> OnSpendCurrency;
        private int _money;

        public double GetCurrency() => _money;

        public void AppendCurrency(int currency)
        {
            _money += currency;
            OnAppendCurrency?.Invoke(currency);
        }

        public void SpendCurrency(int currency)
        {
            _money -= currency;
            OnSpendCurrency?.Invoke(currency);
        }
    }
}