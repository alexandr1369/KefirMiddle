using Zenject;

namespace Factory
{
    public class EnemiesFactory : IFactory<Enemy.Enemy>
    {
        private DiContainer _diContainer;

        [Inject]
        private void Construct(DiContainer diContainer) => _diContainer = diContainer;

        public Enemy.Enemy Create() => _diContainer.Resolve<Enemy.Enemy>();
    }
}