using System;
using _Project.Scripts.Player;
using Zenject;

namespace _Project.Scripts.Interactables
{
    public class LuggageRack : Interactable, IInitializable, IDisposable
    {
        public event Action<Grabbable> PutToRack;
        
        private InteractionManager _interactionManager;
        private PlayerInventory _playerInventory;

        [Inject]
        private void Construct(InteractionManager interactionManager, PlayerInventory playerInventory)
        {
            _interactionManager = interactionManager;
            _playerInventory = playerInventory;
        }

        public void Initialize()
        {
            _interactionManager.ItemInteracted += OnItemInteracted;    
        }

        private void OnItemInteracted(Interactable interactable)
        {
            if (interactable is Grabbable) return;
            
            var grabbable = _playerInventory.Take();
            
            if (grabbable != null)
            {
                PutToRack?.Invoke(grabbable);
                _interactionManager.UnregisterInteractable(grabbable);
            }
        }

        public void Dispose()
        {
            _interactionManager.ItemInteracted -= OnItemInteracted;
        }
    }
}