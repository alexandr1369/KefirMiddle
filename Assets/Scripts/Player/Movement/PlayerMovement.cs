using System;
using LoadingSystem.Loading.Operations.Home;
using Movement;
using UnityEngine;
using Zenject;

namespace Player.Movement
{
    public class PlayerMovement : IInitializable, IFixedTickable, IPlayerMovement, IPlayerMovementAdapter
    {
        public Core Core { get; }
        public Settings MovementSettings { get; }
        public Vector3 Position => Core.Presenter.Position;
        public Vector3 Velocity => Core.Presenter.Velocity;

        private IMovable _moveBehaviour;

        private PlayerMovement(Core core, Settings settings, HomeSceneLoadingContext context)
        {
            Core = core;
            MovementSettings = settings;
            context.PlayerMovement = this;
            context.PlayerMovementAdapter = this;
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