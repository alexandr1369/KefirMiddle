using System;
using LoadingSystem.Loading.Operations.Home;
using Movement.Behaviour;
using Player;
using UnityEngine;
using Zenject;

namespace Movement
{
    public class PlayerMovement : IInitializable, IFixedTickable, ICoreMovement, IPlayerMovementAdapter
    {
        public Core Core { get; }
        public ICoreMovement.Settings MovementSettings { get; }
        public Vector3 Position => Core.Presenter.Position;
        public Vector3 Velocity => Core.Presenter.Velocity;

        private IMovable _moveBehaviour;

        private PlayerMovement(Core core, ICoreMovement.Settings settings, HomeSceneLoadingContext context)
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
    }
}