using System;
using UnityEngine;

namespace UI
{
    public class CanvasRotator : MonoBehaviour
    {
        private Camera _camera;

        private void Start() => RotateCanvas();
        private void OnEnable() => RotateCanvas();

        private void FixedUpdate() => RotateCanvas();

        private void RotateCanvas()
        {
            if (_camera == null)
                _camera = Camera.main;

            transform.forward = _camera.transform.forward;
        }
    }
}