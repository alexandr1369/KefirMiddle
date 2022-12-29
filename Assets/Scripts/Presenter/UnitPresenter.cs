using Model;
using UnityEngine;
using Utils;
using View;

namespace Presenter
{
    public class UnitPresenter : IUnitPresenter
    {
        public Transform Parent
        {
            get => _view.Rb.transform.parent;
            set => _view.Rb.transform.parent = value;
        }

        public Transform BulletsSpawnPoint => _view.BulletsSpawnPoint;

        public Vector3 Position
        {
            get => _view.Rb.position;
            set => _view.Rb.position = value;
        }
        
        public Vector3 Velocity
        {
            get => _view.Rb.velocity;
            set => _view.Rb.velocity = value;
        }
        
        public Quaternion Rotation
        {
            get => _view.Rb.rotation;
            set => _view.Rb.rotation = value;
        }
        
        public bool IsActive
        {
            get => _view.IsActive;
            set => _view.IsActive = value;
        }

        private readonly IUnitModel _model;
        private readonly IUnitView _view;
        private readonly Pool<IUnitPresenter> _pool;
        
        public UnitPresenter(IUnitModel model, IUnitView view, Pool<IUnitPresenter> pool)
        {
            _view = view;
            _model = model;
            _model.OnDestroyed += OnDestroyed;
            _pool = pool;
        }

        public void TakeDamage(int damage) => _model.TakeDamage(damage);
        
        public void AddForce(Vector3 force) => _view.Rb.AddForce(force);

        private void OnDestroyed() => _pool.Despawn(this);

        public class Pool : Pool<IUnitPresenter>
        {
        }
    }
}