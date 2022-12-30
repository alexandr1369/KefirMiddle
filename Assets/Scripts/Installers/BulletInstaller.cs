using Bullet;
using Factory;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class BulletInstaller : MonoInstaller
    {
        [field: SerializeField] private BulletsManager.Settings Settings { get; set; }
        
        public override void InstallBindings()
        {
            BindBullet();
            BindFactory();
            BindManager();
        }

        private void BindBullet()
        {
            Container.Bind<Bullet.Bullet>()
                .AsTransient()
                .WithArguments(Settings);
        }

        private void BindFactory()
        {
            Container.Bind<Utils.IFactory<Bullet.Bullet>>()
                .To<BulletsFactory>()
                .AsSingle();
        }

        private void BindManager()
        {
            Container.BindInterfacesAndSelfTo<BulletsManager>()
                .AsSingle()
                .WithArguments(Settings);
        }
    }
}