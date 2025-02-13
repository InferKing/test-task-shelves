using _Project.Scripts.Camera;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField] private CameraConfig _cameraConfig;
        [SerializeField] private LayerMask _interactableMask;
        
        private const float RaycastLength = 3.5f;
        
        public override void InstallBindings()
        {
            ICamera cameraController = new TouchCameraController(UnityEngine.Camera.main, _cameraConfig);
            
            Container
                .BindInterfacesAndSelfTo<ICamera>()
                .FromInstance(cameraController)
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<ITickable>()
                .To<TouchCameraController>()
                .FromInstance((TouchCameraController)cameraController)
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<CameraRaycast>()
                .FromInstance(new CameraRaycast(cameraController, RaycastLength, _interactableMask))
                .AsSingle()
                .NonLazy();
        }
    }
}