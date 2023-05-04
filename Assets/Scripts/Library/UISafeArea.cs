using UnityEngine;
using Sirenix.OdinInspector;

namespace Library
{
    public sealed class UISafeArea : MonoBehaviour
    {
        [SerializeField] private bool bottom = true;

        [SerializeField, Range(0, 1), ShowIf(nameof(bottom))]
        private float bottomOffsetMult = 0.5f;

        [SerializeField] private bool top = true;

        [SerializeField, Range(0, 1), ShowIf(nameof(top))]
        private float topOffsetMult = 0.5f;

        private void Awake()
        {
            UpdateSafeArea();
        }

        private void UpdateSafeArea()
        {
            var saveArea = Screen.safeArea;
            var saveAreaRectTransform = GetComponent<RectTransform>();

            if (saveArea.height < Screen.height || saveArea.width < Screen.width)
            {
                var anchorMin = saveArea.position;
                var anchorMax = saveArea.position + saveArea.size;

                anchorMin.x /= Screen.width;
                anchorMin.y = bottom ? (anchorMin.y / Screen.height) * bottomOffsetMult : 0;
                anchorMax.x /= Screen.width;
                var topAnchor = anchorMax.y / Screen.height;
                var topOffset = (1 - topAnchor) * topOffsetMult;
                topAnchor += topOffset;
                anchorMax.y = top ? topAnchor : 1;

                saveAreaRectTransform.anchorMin = anchorMin;
                saveAreaRectTransform.anchorMax = anchorMax;
            }
        }
    }
}