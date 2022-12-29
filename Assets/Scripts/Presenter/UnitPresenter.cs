using Model;
using UnityEngine;
using Utils;
using View;

namespace Presenter
{
    public class UnitPresenter : IUnitPresenter
    {
        public IUnitModel Model { get; }
        public IUnitView View { get; }
        
        public Vector3 Position
        {
            get => View.Rb.position;
            set => View.Rb.position = value;
        }
        
        public Vector3 Velocity
        {
            get => View.Rb.velocity;
            set => View.Rb.velocity = value;
        }
        
        public Quaternion Rotation
        {
            get => View.Rb.rotation;
            set => View.Rb.rotation = value;
        }
        
        public bool IsActive
        {
            get => View.IsActive;
            set => View.IsActive = value;
        }

        private readonly Pool<IUnitPresenter> _pool;

        public UnitPresenter(IUnitModel model, IUnitView view, Pool<IUnitPresenter> pool)
        {
            View = view;
            Model = model;
            Model.OnDestroyed += OnDestroyed;
            _pool = pool;
        }

        public void TakeDamage(int damage) => Model.TakeDamage(damage);

        private void OnDestroyed() => _pool.Despawn(this);

        public class Pool : Pool<IUnitPresenter>
        {
        }
    }
}