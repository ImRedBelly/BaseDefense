using DG.Tweening;
using Library;
using Library.Data;
using TMPro;
using UnityEngine;
using Zenject;

namespace Core.UI
{
    public class MoneyController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currencyText;
        [Inject] private SessionManager _sessionManager;



        private void OnEnable()
        {
            ChangeMoneyView();
            _sessionManager.CurrencyDataModel.OnAppendCurrency += OnAppendCurrency;
            _sessionManager.CurrencyDataModel.OnSpendCurrency += OnSpendCurrency;
        }


        private void OnDisable()
        {
            _sessionManager.CurrencyDataModel.OnAppendCurrency -= OnAppendCurrency;
            _sessionManager.CurrencyDataModel.OnSpendCurrency -= OnSpendCurrency;
        }

        private void ChangeMoneyView()
        {
            currencyText.text = TextUtils.FormatShort(_sessionManager.CurrencyDataModel.GetCurrency());
        }

        private void OnAppendCurrency(int price)
        {
            var startValue = (float) _sessionManager.CurrencyDataModel.GetCurrency() - (float) price;
            DOTween.To(Setter, startValue, (float) _sessionManager.CurrencyDataModel.GetCurrency(), 1f)
                .SetUpdate(true);
        }

        private void OnSpendCurrency(int price)
        {
            var startValue = (float) _sessionManager.CurrencyDataModel.GetCurrency() + (float) price;
            DOTween.To(Setter, startValue, (float) _sessionManager.CurrencyDataModel.GetCurrency(), 1f)
                .SetUpdate(true);
 }

        private void Setter(float i) => currencyText.text = TextUtils.FormatShort(i);
        
    }
}
