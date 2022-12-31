using Presenter;
using Utils.Pool;
using Zenject;

namespace Factory
{
    public class UnitPresenterFactory : IFactory<IUnitPresenter>
    {
        private UnitPresenterPool<IUnitPresenter> _pool;

        [Inject]
        private void Construct(UnitPresenterPool<IUnitPresenter> unitPresenterPool) => _pool = unitPresenterPool;
            
        public IUnitPresenter Create() => _pool.Spawn();
    }
}