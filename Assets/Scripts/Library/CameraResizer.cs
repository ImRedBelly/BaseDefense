using UnityEngine;

namespace Library
{
    public class CameraResizer : MonoBehaviour
    {
        public Vector2 defaultResolution = new Vector2(1080, 1920);
        [Range(0f, 1f)] public float widthOrHeight = 0;

        public Camera componentCamera;

        private float _initialSize;
        private float _targetAspect;

        private float _initialFov;
        private float _horizontalFov = 120f;

        private void Start()
        {
            _initialSize = componentCamera.orthographicSize;
            _targetAspect = defaultResolution.x / defaultResolution.y;

            _initialFov = componentCamera.fieldOfView;
            _horizontalFov = CalcVerticalFov(_initialFov, 1 / _targetAspect);
        }

        private void Update()
        {
            if (componentCamera.orthographic)
            {
                float constantWidthSize = _initialSize * (_targetAspect / componentCamera.aspect);
                componentCamera.orthographicSize = Mathf.Lerp(constantWidthSize, _initialSize, widthOrHeight);
            }
            else
            {
                float constantWidthFov = CalcVerticalFov(_horizontalFov, componentCamera.aspect);
                componentCamera.fieldOfView = Mathf.Lerp(constantWidthFov, _initialFov, widthOrHeight);
            }
        }

        private float CalcVerticalFov(float hFovInDeg, float aspectRatio)
        {
            float hFovInRads = hFovInDeg * Mathf.Deg2Rad;

            float vFovInRads = 2 * Mathf.Atan(Mathf.Tan(hFovInRads / 2) / aspectRatio);

            return vFovInRads * Mathf.Rad2Deg;
        }
    }
}