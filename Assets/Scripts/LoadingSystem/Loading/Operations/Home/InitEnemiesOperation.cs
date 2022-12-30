using Cysharp.Threading.Tasks;

namespace LoadingSystem.Loading.Operations.Home
{
    public class InitEnemiesOperation : LoadingOperation
    {
        private readonly HomeSceneLoadingContext _context;

        public InitEnemiesOperation(HomeSceneLoadingContext context) => _context = context;

        public override async UniTask Load()
        {
            _context.EnemiesManager.Start();
            
            await UniTask.Yield();
        }
    }
}