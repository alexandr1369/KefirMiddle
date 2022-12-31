using Utils.Pool;
using Zenject;

namespace Factory
{
    public class FxFactory : IFactory<Fx.Fx>
    {
        private MonoPool<Fx.Fx> _pool;

        [Inject]
        private void Construct(MonoPool<Fx.Fx> pool) => _pool = pool;

        public Fx.Fx Create() => _pool.Spawn();
    }
}