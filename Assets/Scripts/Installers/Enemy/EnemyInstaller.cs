using Enemy;
using Factory;
using Movement;
using UnityEngine;
using Zenject;

namespace Installers.Enemy
{
    public class EnemyInstaller : MonoInstaller
    {
        [field: SerializeField] private ICoreMovement.Settings MovementSettings { get; set; }
        [field: SerializeField] private EnemiesManager.Settings Settings { get; set; }
        
        public override void InstallBindings()
        {
            BindEnemy();
            BindFactory();
            BindManager();
            BindTeleportChecker();
        }

        private void BindEnemy()
        {
            Container.Bind<ICoreMovement.Settings>()
                .FromInstance(MovementSettings)
                .AsSingle();
            
            Container.Bind<EnemiesManager.Settings>()
                .FromInstance(Settings)
                .AsSingle();
            
            Container.Bind<global::Enemy.Enemy>()
                .FromSubContainerResolve()
                .ByInstaller<EnemySubContainerInstaller>()
                .AsTransient();
        }

        private void BindFactory()
        {
            Container.Bind<Factory.IFactory<global::Enemy.Enemy>>()
                .To<EnemiesFactory>()
                .AsSingle();
        }

        private void BindManager()
        {
            Container.BindInterfacesAndSelfTo<EnemiesManager>()
                .AsSingle()
                .WithArguments(Settings);
        }

        private void BindTeleportChecker()
        {
            Container.BindInterfacesAndSelfTo<EnemiesTeleportChecker>()
                .AsSingle();
        }
    }
}