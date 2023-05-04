using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HpViewController : MonoBehaviour
    {
        [SerializeField] private Slider sliderHp;
        private Transform _currentTrasnform;

        public void ShowHideHp(bool isActivate)
        {
            if (_currentTrasnform == null)
                _currentTrasnform = transform;

            var scale = isActivate ? Vector3.one : Vector3.zero;
            if (isActivate) _currentTrasnform.DOScale(scale, 0.1f);
            else _currentTrasnform.transform.localScale = scale;
        }

        public void SetProgress(float progress)
        {
            sliderHp.value = progress;
            ShowHideHp(!(sliderHp.value >= sliderHp.maxValue));
        }

        private void OnDisable()
        {
            DOTween.Kill(_currentTrasnform);
        }
    }
}