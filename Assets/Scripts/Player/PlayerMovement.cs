using System;
using Movement;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerMovement : IInitializable, IFixedTickable, IPlayerMovement
    {
        private readonly Core _core;
        private readonly Settings _settings;
        private IMovable _moveBehaviour;

        private PlayerMovement(Core core, Settings settings)
        {
            _core = core;
            _settings = settings;
        }

        public void Initialize() => SetMoveBehaviour(/*new NoMoveBehaviour()*/new PlayerMoveBehaviour(_core, _settings));

        public void FixedTick()
        {
            if(_core.Presenter.IsDead)
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

    public interface IPlayerMovement
    {
        void SetMoveBehaviour(IMovable moveBehaviour);
    }
}