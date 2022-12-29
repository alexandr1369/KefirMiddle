using Cysharp.Threading.Tasks;
using Movement;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerDeathChecker : IInitializable
    {
        private readonly Player _player;
        private readonly IPlayerMovement _movement;

        private PlayerDeathChecker(Player player, IPlayerMovement movement)
        {
            _player = player;
            _movement = movement;
        }

        public async void Initialize()
        {
            await UniTask.Yield();
            
            _player.Presenter.OnDestroyed += OnDestroyed;
        }

        private void OnDestroyed()
        {
            _movement.SetMoveBehaviour(new NoMoveBehaviour());
            
            Debug.Log("Player lost!");
            
            // TODO: restart game with ISceneLoader
            
        }
    }
}