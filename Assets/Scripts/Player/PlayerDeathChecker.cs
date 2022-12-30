using Cysharp.Threading.Tasks;
using LoadingSystem.Loading.Operations.Home;
using Movement;
using Player.Movement;
using Zenject;

namespace Player
{
    public class PlayerDeathChecker : IInitializable
    {
        private readonly Core _core;
        private readonly IPlayerMovement _movement;
        private readonly HomeSceneLoadingContext _context;

        private PlayerDeathChecker(Core core, IPlayerMovement movement, HomeSceneLoadingContext context)
        {
            _core = core;
            _movement = movement;
            _context = context;
        }

        public async void Initialize()
        {
            await UniTask.Yield();
            
            _core.Presenter.OnDestroyed += OnDestroyed;
        }

        private void OnDestroyed()
        {
            _movement.SetMoveBehaviour(new NoMoveBehaviour());
            
            // TODO: defeat dialogue
            _context.SceneLoadingService.RestartCurrentLocation();
        }
    }
}