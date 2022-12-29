using UnityEngine;
using Zenject;

namespace Player.SpawnPoint
{
    public class PlayerSpawnPoint : MonoBehaviour, ISpawnPoint
    {
        private Player _player;
        
        [Inject]
        private void Construct(Player player) => _player = player;

        // TODO: вынести в сиквенс загрузки
        private void Start() => Respawn();

        public void Respawn() => _player.Init(transform);
    }
}