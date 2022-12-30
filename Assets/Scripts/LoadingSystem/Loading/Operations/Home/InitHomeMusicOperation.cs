using Cysharp.Threading.Tasks;

namespace LoadingSystem.Loading.Operations.Home
{
    public class InitHomeMusicOperation : LoadingOperation
    {
        private readonly HomeSceneLoadingContext _context;
        
        public InitHomeMusicOperation(HomeSceneLoadingContext context) => _context = context;

        public override async UniTask Load()
        {
            _context.AudioService.PreloadHomeAudio();
            _context.AudioService.PlayHomeMusic();
         
            await UniTask.Yield();
        }
    }
}