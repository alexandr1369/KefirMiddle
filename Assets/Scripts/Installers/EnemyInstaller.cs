using Enemy;
using Factory;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class EnemyInstaller : MonoInstaller
    {
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
            Container.BindInterfacesAndSelfTo<Enemy.Enemy>()
                .AsTransient()
                .WithArguments(Settings);
        }

        private void BindFactory()
        {
            Container.Bind<Factory.IFactory<Enemy.Enemy>>()
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