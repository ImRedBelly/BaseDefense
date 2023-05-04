using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class CanvasRotator : MonoBehaviour
    {
        private Camera _camera;
        private Transform _currentTrasnform;

        private void OnEnable() => RotateCanvas();

        private void FixedUpdate() => RotateCanvas();

        private void RotateCanvas()
        {
            if (_currentTrasnform == null)
                _currentTrasnform = transform;
            if (_camera == null)
                _camera = Camera.main;

            _currentTrasnform.forward = _camera.transform.forward;
        }
    }
}