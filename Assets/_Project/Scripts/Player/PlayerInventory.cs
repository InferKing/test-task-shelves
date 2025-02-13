using System;
using System.Collections.Generic;
using _Project.Scripts.Interactables;
using Zenject;

namespace _Project.Scripts.Player
{
    public class PlayerInventory : IInitializable, IDisposable
    {
        private Stack<Grabbable> _inventory = new();
        private InteractionManager _manager;
        
        public PlayerInventory(InteractionManager manager)
        {
            _manager = manager;
        }

        public void Initialize()
        {
            _manager.ItemInteracted += OnItemInteracted;    
        }

        private void OnItemInteracted(Interactable interactable)
        {
            if (interactable is not Grabbable grabbable) return;
            
            Put(grabbable);
            interactable.gameObject.SetActive(false);
        }

        public void Put(Grabbable grabbable)
        {
            _inventory.Push(grabbable);
        }

        public Grabbable Take()
        {
            return _inventory.Count == 0 ? null : _inventory.Pop();
        }

        public void Dispose()
        {
            _manager.ItemInteracted -= OnItemInteracted;
        }
    }
}