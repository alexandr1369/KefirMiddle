using UnityEngine;
using Zenject;

namespace Player.SpawnPoint
{
    public class PlayerSpawnPoint : MonoBehaviour, ISpawnPoint
    {
        private Core _core;
        
        [Inject]
        private void Construct(Core core) => _core = core;

        // TODO: вынести в сиквенс загрузки
        private void Start() => Respawn();

        public void Respawn() => _core.Init(transform);
    }
}