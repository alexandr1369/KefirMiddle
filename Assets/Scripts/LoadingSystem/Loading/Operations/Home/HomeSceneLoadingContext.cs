using Audio;
using Enemy;
using Fx;
using Movement;
using Player;
using Player.Shooting;
using Player.Shooting.ExtraShooting;
using Player.Turning;
using StateSystem;

namespace LoadingSystem.Loading.Operations.Home
{
    public class HomeSceneLoadingContext
    {
        public ISceneLoadingService SceneLoadingService { get; set; }
        public IAudioService AudioService { get; set; }
        public IFxService FxService { get; set; }
        public ICoreMovement PlayerMovement { get; set; }
        public IPlayerMovementAdapter PlayerMovementAdapter { get; set; }
        public IPlayerRotationAdapter PlayerRotationAdapter { get; set; }
        public IPlayerShooting PlayerShooting { get; set; }
        public IPlayerExtraShootingAdapter PlayerExtraShootingAdapter { get; set; }
        public IEnemiesManager EnemiesManager { get; set; }
    }
    
}