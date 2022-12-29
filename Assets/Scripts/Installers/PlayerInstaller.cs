using Player;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [field: SerializeField] private PlayerMovement.Settings MovementSettings { get; set; }
        [field: SerializeField] private PlayerShooting.Settings ShootingSettings { get; set; }
        
        public override void InstallBindings()
        {
            BindPlayer();
            BindMovement();
            BindShooting();
        }

        private void BindPlayer()
        {
            Container.Bind<Player.Player>()
                .AsSingle();
        }

        private void BindMovement()
        {
            Container.BindInterfacesAndSelfTo<PlayerMovement>()
                .AsSingle()
                .WithArguments(MovementSettings);
        }

        private void BindShooting()
        {
            Container.BindInterfacesAndSelfTo<PlayerShooting>()
                .AsSingle()
                .WithArguments(ShootingSettings);
        }
    }
}