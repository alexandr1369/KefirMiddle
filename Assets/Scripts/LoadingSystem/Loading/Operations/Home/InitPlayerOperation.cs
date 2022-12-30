using Cysharp.Threading.Tasks;
using Movement;

namespace LoadingSystem.Loading.Operations.Home
{
    public class InitPlayerOperation : LoadingOperation
    {
        private readonly HomeSceneLoadingContext _context;

        public InitPlayerOperation(HomeSceneLoadingContext context) => _context = context;

        public override async UniTask Load()
        {
            var movement = _context.PlayerMovement;
            var shooting = _context.PlayerShooting;
            movement.SetMoveBehaviour(new PlayerMoveBehaviour(movement.Core, movement.MovementSettings));
            shooting.Start();
            
            await UniTask.Yield();
        }
    }
}