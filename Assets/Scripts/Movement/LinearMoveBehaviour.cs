using Presenter;
using UnityEngine;

namespace Movement
{
    public class LinearMoveBehaviour : IMovable
    {
        private readonly IUnitPresenter _presenter;
        private readonly Vector3 _direction;
        private readonly float _speed;

        public LinearMoveBehaviour(IUnitPresenter presenter, Vector3 direction, float speed)
        {
            _presenter = presenter;
            _direction = direction;
            _speed = speed;
        }
        
        public void Move() => _presenter.Velocity = _direction * _speed;
    }
}