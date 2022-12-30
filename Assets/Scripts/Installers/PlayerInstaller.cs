using Player;
using Player.SpawnPoint;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [field: SerializeField] private PlayerMovement.Settings MovementSettings { get; set; }
        [field: SerializeField] private PlayerShooting.Settings ShootingSettings { get; set; }
        [field: SerializeField] private PlayerSpawnPoint PlayerSpawnPoint { get; set; }

        public override void InstallBindings()
        {
            BindSpawnPoint();
            BindPlayer();
            BindMovement();
            BindTurning();
            BindShooting();
            BindDeathChecker();
            BindTeleportChecker();
        }

        private void BindSpawnPoint()
        {
            Container.Bind<ISpawnPoint>()
                .To<PlayerSpawnPoint>()
                .FromInstance(PlayerSpawnPoint)
                .AsSingle();
        }

        private void BindPlayer()
        {
            Container.Bind<Core>()
                .AsSingle();
        }

        private void BindMovement()
        {
            Container.BindInterfacesAndSelfTo<PlayerMovement>()
                .AsSingle()
                .WithArguments(MovementSettings);
        }

        private void BindTurning()
        {
            Container.BindInterfacesAndSelfTo<PlayerTurning>()
                .AsSingle();
        }

        private void BindShooting()
        {
            Container.BindInterfacesAndSelfTo<PlayerShooting>()
                .AsSingle()
                .WithArguments(ShootingSettings);
        }

        private void BindDeathChecker()
        {
            Container.Bind(typeof(IInitializable))
                .To<PlayerDeathChecker>()
                .AsSingle();
        }

        private void BindTeleportChecker()
        {
            Container.BindInterfacesAndSelfTo<PlayerTeleportChecker>()
                .AsSingle();
        }
    }
}