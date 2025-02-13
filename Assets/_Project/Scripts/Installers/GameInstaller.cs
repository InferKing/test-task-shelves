using _Project.Scripts.Interactables;
using _Project.Scripts.Player;
using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private LuggageRack _luggageRack;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<InteractionManager>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<PlayerInventory>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<LuggageRack>()
                .FromComponentInNewPrefab(_luggageRack)
                .AsSingle();
            
            Container
                .BindInterfacesTo<GameUI>()
                .FromComponentInNewPrefab(_gameUI)
                .AsSingle();
        }
    }
}