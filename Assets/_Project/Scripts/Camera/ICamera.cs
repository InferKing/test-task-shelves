using System;
using UnityEngine;

namespace _Project.Scripts.Camera
{
    public interface ICamera
    {
        public event Action<Quaternion> RotationChanged;
        UnityEngine.Camera Camera { get; }
        void Rotate();
    }
}