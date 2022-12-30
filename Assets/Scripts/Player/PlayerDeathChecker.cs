using Cysharp.Threading.Tasks;
using Movement;
using Player.Movement;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerDeathChecker : IInitializable
    {
        private readonly Core _core;
        private readonly IPlayerMovement _movement;

        private PlayerDeathChecker(Core core, IPlayerMovement movement)
        {
            _core = core;
            _movement = movement;
        }

        public async void Initialize()
        {
            await UniTask.Yield();
            
            _core.Presenter.OnDestroyed += OnDestroyed;
        }

        private void OnDestroyed()
        {
            _movement.SetMoveBehaviour(new NoMoveBehaviour());
            
            Debug.Log("Player lost!");
            
            // TODO: restart game with ISceneLoader
            
        }
    }
}