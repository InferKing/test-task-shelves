using UnityEngine;

namespace _Project.Scripts.Camera
{
    [CreateAssetMenu(fileName = "CameraConfig", menuName = "Config/CameraConfig")]
    public class CameraConfig : ScriptableObject
    {
        [field: SerializeField] public float Sensitivity { get; private set; } = 250;
        [field: SerializeField] public int ClampAngle { get; private set; } = 90;
    }
}