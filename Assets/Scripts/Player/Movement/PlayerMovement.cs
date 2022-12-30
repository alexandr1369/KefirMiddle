using System;
using LoadingSystem.Loading.Operations.Home;
using Movement;
using UnityEngine;
using Zenject;

namespace Player.Movement
{
    public class PlayerMovement : IInitializable, IFixedTickable, IPlayerMovement
    {
        public Core Core { get; }
        public Settings MovementSettings { get; }
        
        private IMovable _moveBehaviour;

        private PlayerMovement(Core core, Settings settings, HomeSceneLoadingContext context)
        {
            Core = core;
            MovementSettings = settings;
            context.PlayerMovement = this;
        }

        public void Initialize() => SetMoveBehaviour(new NoMoveBehaviour());

        public void FixedTick()
        {
            if(Core.Presenter.IsDead)
                return;
            
            _moveBehaviour.Move();
        }

        public void SetMoveBehaviour(IMovable moveBehaviour) => _moveBehaviour = moveBehaviour;

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float MovementSpeed { get; private set; }   
        }
    }
}