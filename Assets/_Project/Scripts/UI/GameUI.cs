using System.Text;
using _Project.Scripts.Interactables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI
{
    public class GameUI : MonoBehaviour, IInitializable
    {
        [SerializeField] private Button _interactButton;
        [SerializeField] private TMP_Text _showText;
        
        private InteractionManager _interactionManager;
        private LuggageRack _luggageRack;
        private Interactable _interactable;

        [Inject]
        private void Construct(InteractionManager interactionManager, LuggageRack luggageRack)
        {
            _interactionManager = interactionManager;
            _luggageRack = luggageRack;
        }

        public void Initialize()
        {
            _interactionManager.ItemEntered += OnItemEntered;
            _interactionManager.ItemExited += OnItemExited;
            
            _luggageRack.PutToRack += OnPutToRack;
        }

        private void OnPutToRack(Grabbable grabbable)
        {
            var text = new StringBuilder(_showText.text);
            
            text.AppendLine($"\nAdded {grabbable.gameObject.name} to luggage rack");
            
            _showText.text = text.ToString();
        }

        public void Interact()
        {
            if (_interactable == null) return;
            
            _interactable.Interact();
        }

        private void OnItemExited(Interactable interactable)
        {
            _interactButton.gameObject.SetActive(false);
         
            _interactable = null;
        }

        private void OnItemEntered(Interactable interactable)
        {
            _interactButton.gameObject.SetActive(true);
            
            _interactable = interactable;
        }

        private void OnDestroy()
        {
            _interactionManager.ItemEntered -= OnItemEntered;
            _interactionManager.ItemExited -= OnItemExited;
            
            _luggageRack.PutToRack -= OnPutToRack;
        }
    }
}