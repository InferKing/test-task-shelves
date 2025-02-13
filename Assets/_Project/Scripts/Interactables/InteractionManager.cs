using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Interactables
{
    public class InteractionManager : IInitializable
    {
        private List<Interactable> _interactables = new();
        
        public event Action<Interactable> ItemInteracted;
        public event Action<Interactable> ItemEntered;
        public event Action<Interactable> ItemExited;
        
        public void RegisterInteractable(Interactable interactable)
        {
            if (!_interactables.Contains(interactable))
            {
                _interactables.Add(interactable);
                
                interactable.Interacted += OnItemInteracted;
                interactable.Enter += OnItemEntered;
                interactable.Exit += OnItemExited;

            }
        }

        private void OnItemExited(Interactable interactable)
        {
            ItemExited?.Invoke(interactable);
        }

        private void OnItemEntered(Interactable interactable)
        {
            ItemEntered?.Invoke(interactable);
        }

        private void OnItemInteracted(Interactable interactable)
        {
            ItemInteracted?.Invoke(interactable);
        }

        public void UnregisterInteractable(Interactable interactable)
        {
            if (_interactables.Contains(interactable))
            {
                _interactables.Remove(interactable);
                
                interactable.Interacted -= OnItemInteracted;
                interactable.Enter -= OnItemEntered;
                interactable.Exit -= OnItemExited;
            }
        }

        public void Initialize()
        {
            var data = Object.FindObjectsByType<Interactable>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
                .ToList();
            
            foreach (var interactable in data)
            {
                RegisterInteractable(interactable);
            }
        }
    }
}