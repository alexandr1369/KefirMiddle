using Presenter;
using Utils;
using Zenject;

namespace Factory
{
    public class UnitPresenterFactory : IFactory<IUnitPresenter>
    {
        private Pool<IUnitPresenter> _pool;

        [Inject]
        private void Construct(Pool<IUnitPresenter> pool) => _pool = pool;
            
        public IUnitPresenter Create() => _pool.Spawn();
    }
}