using System;
using Lean.Pool;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIInteractionController : MonoBehaviour
    {
        [SerializeField] private Image sliderImage;
        [SerializeField] private CanvasGroup canvasGroup;

        private Sequence _sequence;

        public void Show()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();

            _sequence.Append(transform.DOScale(Vector3.one, 0));
            _sequence.Join(sliderImage.DOFillAmount(0, 0f));
            _sequence.Join(canvasGroup.DOFade(1, 0f));
        }

        public void SetProgress(float progress)
        {
            sliderImage.fillAmount = progress;
        }

        public void OnCompletedSlider()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DOScale(Vector3.one * 1.5f, 0.25f).SetEase(Ease.Linear));
            _sequence.Join(canvasGroup.DOFade(0, 0.25f));
        }
    }
}