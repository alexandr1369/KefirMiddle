using System;
using Model;
using UnityEngine;
using Utils;
using View;

namespace Presenter
{
    public class UnitPresenter : IUnitPresenter
    {
        public event Action OnDestroyed;

        public Transform Parent
        {
            get => _view.Rb.transform.parent;
            set => _view.Rb.transform.parent = value;
        }

        public Transform BulletsSpawnPoint => _view.BulletsSpawnPoint;
        
        public Transform Transform => _view.Rb.transform;

        public MeshRenderer MeshRenderer => _view.MeshRenderer;
        public MeshFilter MeshFilter => _view.MeshFilter;

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

        public Vector3 LocalScale
        {
            get => _view.Rb.transform.localScale;
            set => _view.Rb.transform.localScale = value;
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

        public UnitPresenter(IUnitModel model, IUnitView view, Pool<IUnitPresenter> pool)
        {
            _view = view;
            _model = model;
            OnDestroyed += () => pool.Despawn(this);
            _model.OnDestroyed += OnDestroyed;
        }

        public void TakeDamage(int damage) => _model.TakeDamage(damage);
        
        public void AddForce(Vector3 force) => _view.Rb.AddForce(force);

        public class Pool : Pool<IUnitPresenter>
        {
        }
    }
}