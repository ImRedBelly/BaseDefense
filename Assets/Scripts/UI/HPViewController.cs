using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class HPViewController : MonoBehaviour
    {
        [SerializeField] private Slider sliderHP;
        [SerializeField] private CanvasGroup canvasGroup;

        public void ShowHideHP(bool isActivate)
        {
            canvasGroup.alpha = isActivate ? 1 : 0;
        }

        public void SetProgress(float progress)
        {
            sliderHP.value = progress;
        }
    }
}