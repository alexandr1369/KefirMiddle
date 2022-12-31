using Audio;
using Enemy;
using Player.Movement;
using Player.Shooting;
using Player.Shooting.ExtraShooting;
using StateSystem;

namespace LoadingSystem.Loading.Operations.Home
{
    public class HomeSceneLoadingContext
    {
        public ISceneLoadingService SceneLoadingService { get; set; }
        public IAudioService AudioService { get; set; }
        public IPlayerMovement PlayerMovement { get; set; }
        public IPlayerShooting PlayerShooting { get; set; }
        public IPlayerExtraShootingAdapter PlayerExtraShootingAdapter { get; set; }
        public IEnemiesManager EnemiesManager { get; set; }
    }
    
}