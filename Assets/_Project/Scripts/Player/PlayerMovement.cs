using _Project.Scripts.Camera;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private Joystick _joystick;
        
        private const float Gravity = 9.81f;
        
        private Vector3 _velocity;
        private Quaternion _cameraRotation;
        private ICamera _camera;

        [Inject]
        private void Construct(ICamera camera)
        {
            _camera = camera;
        }
        
        private void Start()
        {
            _camera.RotationChanged += OnRotationChanged;
        }

        private void Update()
        {
            MoveTo(_joystick.Horizontal, _joystick.Vertical);
        }

        private void OnRotationChanged(Quaternion cameraRotation)
        {
            _cameraRotation = cameraRotation;
        }

        private void MoveTo(float horizontal, float vertical)
        {
            Vector3 move = _cameraRotation * new Vector3(horizontal, 0, vertical);
    
            if (_controller.isGrounded && _velocity.y < 0)
            {
                _velocity.y = -1f;
            }

            _velocity.y -= Gravity * Time.deltaTime;

            _controller.Move((move + _velocity) * Time.deltaTime);
        }

        private void OnDestroy()
        {
            _camera.RotationChanged -= OnRotationChanged;
        }
    }
}