using System;
using UnityEngine;

namespace _Project.Scripts.Interactables
{
    public abstract class Interactable : MonoBehaviour
    {
        public event Action<Interactable> Interacted;
        public event Action<Interactable> Enter;
        public event Action<Interactable> Exit;

        public virtual void Interact()
        {
            Interacted?.Invoke(this);
        }

        public virtual void OnEnter()
        {
            Enter?.Invoke(this);
        }

        public virtual void OnExit()
        {
            Exit?.Invoke(this);
        }
    }
}