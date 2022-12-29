using System;
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
            BindEnemiesManager();
        }

        private void BindEnemy()
        {
            Container.Bind<Enemy.Enemy>()
                .AsTransient()
                .WithArguments(Settings);
        }

        private void BindFactory()
        {
            Container.Bind<Utils.IFactory<Enemy.Enemy>>()
                .To<EnemiesFactory>()
                .AsSingle();
        }

        private void BindEnemiesManager()
        {
            Container.BindInterfacesAndSelfTo<EnemiesManager>()
                .AsSingle()
                .WithArguments(Settings);
        }
    }
}