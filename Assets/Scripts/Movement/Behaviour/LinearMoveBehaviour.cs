using Presenter;
using UnityEngine;

namespace Movement.Behaviour
{
    public class LinearMoveBehaviour : IMovable
    {
        private readonly IUnitPresenter _presenter;
        private readonly Vector3 _velocity;

        public LinearMoveBehaviour(IUnitPresenter presenter, Vector3 velocity)
        {
            _presenter = presenter;
            _velocity = velocity;
        }
        
        public void Move() => _presenter.Velocity = _velocity;
    }
}