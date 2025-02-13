using _Project.Scripts.Interactables;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Camera
{
    public class CameraRaycast : ITickable
    {
        private readonly float _rayLength;
        private readonly LayerMask _layerMask;
        
        private readonly ICamera _camera;
        private Interactable _lastInteractable;
        
        public CameraRaycast(ICamera camera, float rayLength, LayerMask layerMask)
        {
            _camera = camera;
            _rayLength = rayLength;
            _layerMask = layerMask;
        }
        
        public void Tick()
        {
            Transform cameraTransform = _camera.Camera.transform;

            FindInteractable(cameraTransform);
        }

        private void FindInteractable(Transform cameraTransform)
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit, _rayLength, _layerMask))
            {
                if (!_lastInteractable) return;
                
                _lastInteractable?.OnExit();
                _lastInteractable = null;
                
                return;
            }

            if (!hit.collider) return;

            var interactable = hit.collider.GetComponent<Interactable>();
            
            if (HasNewInteractable(interactable))
            {
                _lastInteractable?.OnExit();
                _lastInteractable = interactable;
                _lastInteractable.OnEnter();
            }
            else if (!_lastInteractable)
            {
                _lastInteractable = interactable;
                _lastInteractable.OnEnter();
            }
        }

        private bool HasNewInteractable(Interactable interactable)
        {
            return _lastInteractable && _lastInteractable != interactable;
        }
    }
}