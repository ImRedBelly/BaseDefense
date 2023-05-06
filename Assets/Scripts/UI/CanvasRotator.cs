using UnityEngine;

namespace UI
{
    public class CanvasRotator : MonoBehaviour
    {
        private Transform _currentTrasnform;
        private Transform _cameraTrasnform;

        private void OnEnable() => RotateCanvas();

        private void FixedUpdate() => RotateCanvas();

        private void RotateCanvas()
        {
            if (_currentTrasnform == null)
                _currentTrasnform = transform;
            if (_cameraTrasnform == null)
                _cameraTrasnform = Camera.main.transform;

            _currentTrasnform.forward = _cameraTrasnform.forward;
        }
    }
}