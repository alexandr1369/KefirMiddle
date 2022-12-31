using Player;
using Zenject;

namespace Movement
{
    public class EnemyMovement : IFixedTickable, ICoreMovement
    {
        public Core Core { get; }
        public ICoreMovement.Settings MovementSettings { get; }
        
        private IMovable _moveBehaviour;

        public EnemyMovement(Core core, ICoreMovement.Settings settings)
        {
            Core = core;
            MovementSettings = settings;
        }

        public void FixedTick()
        {
            if(Core.Presenter.IsDead)
                return;
            
            _moveBehaviour.Move();
        }

        public void SetMoveBehaviour(IMovable moveBehaviour) => _moveBehaviour.Move();
    }
}