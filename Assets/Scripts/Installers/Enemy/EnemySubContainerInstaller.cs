using Enemy;
using Movement;
using Zenject;

namespace Installers.Enemy
{
    public class EnemySubContainerInstaller : Installer
    {
        private ICoreMovement.Settings _movementSettings;
        private EnemiesManager.Settings _enemiesManagerSettings;

        [Inject]
        private void Construct(ICoreMovement.Settings movementSettings, EnemiesManager.Settings enemiesManagerSettings)
        {
            _movementSettings = movementSettings;
            _enemiesManagerSettings = enemiesManagerSettings;
        }
        
        public override void InstallBindings()
        {
            BindMovement();
            BindEnemy();
        }

        private void BindMovement()
        {
            Container.Bind<EnemyMovement>()
                .AsSingle()
                .WithArguments(_movementSettings);
        }

        private void BindEnemy()
        {
            Container.BindInterfacesAndSelfTo<global::Enemy.Enemy>()
                .AsSingle()
                .WithArguments(_enemiesManagerSettings);
        }
    }
}