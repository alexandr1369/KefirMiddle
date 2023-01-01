using Presenter;

namespace Movement.Behaviour
{
    public class FollowingPlayerMoveBehaviour : IMovable
    {
        private readonly IUnitPresenter _presenter;
        private readonly IPlayerMovementAdapter _playerMovementAdapter;
        private readonly float _velocity;

        public FollowingPlayerMoveBehaviour(IUnitPresenter presenter, IPlayerMovementAdapter playerMovementAdapter,
            float velocity)
        {
            _presenter = presenter;
            _playerMovementAdapter = playerMovementAdapter;
            _velocity = velocity;
        }
        
        public void Move()
        {
            var distanceToPlayer = (_playerMovementAdapter.Position - _presenter.Position).normalized;
            _presenter.Velocity = distanceToPlayer * _velocity;
        }
    }
}