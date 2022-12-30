using Zenject;

namespace Factory
{
    public class BulletsFactory : IFactory<Bullet.Bullet>
    {
        private DiContainer _diContainer;

        [Inject]
        private void Construct(DiContainer diContainer) => _diContainer = diContainer;

        public Bullet.Bullet Create() => _diContainer.Resolve<Bullet.Bullet>();
    }
}