using Bullet;
using Bullet.LifeTimeChecker;
using Factory;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class BulletInstaller : MonoInstaller
    {
        [field: SerializeField] private BulletsService.Settings Settings { get; set; }
        
        public override void InstallBindings()
        {
            BindBullet();
            BindFactory();
            BindManager();
            BindLifeTimeChecker();
        }

        private void BindBullet()
        {
            Container.Bind<Bullet.Bullet>()
                .AsTransient()
                .WithArguments(Settings);
        }

        private void BindFactory()
        {
            Container.Bind<Factory.IFactory<Bullet.Bullet>>()
                .To<BulletsFactory>()
                .AsSingle();
        }

        private void BindManager()
        {
            Container.BindInterfacesAndSelfTo<BulletsService>()
                .AsSingle()
                .WithArguments(Settings);
        }

        private void BindLifeTimeChecker()
        {
            Container.BindInterfacesAndSelfTo<BulletsLifeTimeChecker>()
                .AsSingle()
                .WithArguments(Settings);
        }
    }
}