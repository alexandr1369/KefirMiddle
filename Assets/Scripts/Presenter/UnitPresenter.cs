using System;
using Model;
using UnityEngine;
using Utils;
using View;

namespace Presenter
{
    /// <summary>
    /// Base presenter for all units.
    /// </summary>
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
            get => _view.Rb.transform.position;
            set => _view.Rb.transform.position = value;
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
            get => _view.Rb.transform.rotation;
            set => _view.Rb.transform.rotation = value;
        }

        public Vector3 Direction
        {
            get => -_view.Rb.transform.right;
            set => _view.Rb.transform.right = -value;
        }

        public AudioSource AudioSource => _view.AudioSource;

        public float Drag
        {
            get => _view.Rb.drag;
            set => _view.Rb.drag = value;
        }

        public bool IsActive
        {
            get => _view.IsActive;
            set => _view.IsActive = value;
        }

        public bool IsPlayer
        {
            get => _view.IsPlayer;
            set
            {
                _view.IsPlayer = value;
                SetCoreEvent(value);
            }
        }

        public bool IsBullet
        {
            set
            {
                _view.IsBullet = value;
                SetCoreEvent(IsPlayer && !value);
            }
        }

        public bool IsDead => _model.Health <= 0;

        private readonly IUnitModel _model;
        private readonly IUnitView _view;

        public UnitPresenter(IUnitModel model, IUnitView view, Pool<IUnitPresenter> pool)
        {
            _view = view;
            _model = model;
            _model.OnDestroyed += () => OnDestroyed?.Invoke();
            OnDestroyed += () => pool.Despawn(this);
        }

        public void TakeDamage() => _model.TakeDamage();
        
        public void AddForce(Vector3 force) => _view.Rb.AddForce(force);

        private void SetCoreEvent(bool isPlayerHitEnemy)
        {
            _view.OnPlayerHitsEnemy -= TakeDamage;
            _view.OnEnemyHitsBullet -= TakeDamage;
            
            if(isPlayerHitEnemy)
                _view.OnPlayerHitsEnemy += TakeDamage;
            else
                _view.OnEnemyHitsBullet += TakeDamage;
        }

        public class Pool : Pool<IUnitPresenter>
        {
        }
    }
}