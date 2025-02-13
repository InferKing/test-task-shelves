using System;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Camera
{
    public class TouchCameraController : ICamera, ITickable
    {
        public event Action<Quaternion> RotationChanged;
        
        private float _rotationX; 
    
        private readonly Transform _cameraTransform;
        private readonly CameraConfig _config;
        private float _initialPitch;
        private int? _cameraTouchId;

        private float _currentYaw;
        private float _currentPitch;
    
        public TouchCameraController(UnityEngine.Camera camera, CameraConfig config)
        {
            Camera = camera;
            _config = config;
        
            _cameraTransform = Camera.transform;
            
            InitRotation();
        }

        private void InitRotation()
        {
            Vector3 euler = _cameraTransform.rotation.eulerAngles;
            var initialYaw = NormalizeAngle(euler.y);
            _initialPitch = NormalizeAngle(euler.x);

            _currentYaw = initialYaw;
            _currentPitch = _initialPitch;
        }

        public UnityEngine.Camera Camera { get; }
    
        public void Tick()
        {
            Rotate();
        }
        
        public void Rotate()
        {
            if (Input.touchCount <= 0) return;

            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                if (_cameraTouchId.HasValue && touch.fingerId == _cameraTouchId.Value)
                {
                    RotateByTouch(touch);
                    break;
                }

                if (!_cameraTouchId.HasValue && touch.phase == TouchPhase.Began && touch.position.x > Screen.width / 2)
                {
                    _cameraTouchId = touch.fingerId;
                }
            }

            if (_cameraTouchId.HasValue)
            {
                Touch cameraTouch = Input.GetTouch(_cameraTouchId.Value);
                if (cameraTouch.phase == TouchPhase.Ended || cameraTouch.phase == TouchPhase.Canceled)
                {
                    _cameraTouchId = null;
                }
            }
        }

        private void RotateByTouch(Touch touch)
        {
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = touch.deltaPosition;

                _currentYaw += delta.x * _config.Sensitivity;
                _currentPitch -= delta.y * _config.Sensitivity;

                _currentPitch = Mathf.Clamp(_currentPitch, _initialPitch - _config.ClampAngle, _initialPitch + _config.ClampAngle);

                _cameraTransform.rotation = Quaternion.Euler(_currentPitch, _currentYaw, 0f);
            }
            
            RotationChanged?.Invoke(_cameraTransform.localRotation);
        }
        
        private float NormalizeAngle(float angle)
        {
            while (angle > 180f)
                angle -= 360f;
            while (angle < -180f)
                angle += 360f;
            return angle;
        }
    }
}