using System;
using Fx;
using LoadingSystem.Loading.Operations.Home;
using UnityEngine;
using Zenject;

namespace View
{
    /// <summary>
    /// Base view for all units.
    /// </summary>
    public class UnitView : MonoBehaviour, IUnitView
    {
        public event Action OnEnemyHitsBullet;
        public event Action OnPlayerHitsEnemy;
        public event Action OnForceDestroy;
        
        [field: SerializeField] public Rigidbody Rb { get; private set; }
        [field: SerializeField] public MeshRenderer MeshRenderer { get; private set; }
        [field: SerializeField] public MeshFilter MeshFilter { get; private set; }
        [field: SerializeField] public Transform BulletsSpawnPoint { get; private set;}
        [field: SerializeField] public AudioSource AudioSource { get; private set; }
        
        private HomeSceneLoadingContext _context;

        [Inject]
        private void Construct(HomeSceneLoadingContext context) => _context = context;

        public bool IsActive
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value); 
        }

        public bool IsPlayer { get; set; }
        
        public bool IsBullet { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            var unitView = other.GetComponent<UnitView>();
            
            if(!unitView)
                return;
            
            InteractWith(unitView);
        }

        private void InteractWith(IUnitView unitView)
        {
            var playerHitsEnemy = IsPlayer && !unitView.IsPlayer && !unitView.IsBullet;
            var enemyHitsBullet = !IsPlayer && !IsBullet && unitView.IsBullet;

            if (!playerHitsEnemy && !enemyHitsBullet)
                return;
            
            if (playerHitsEnemy) 
                OnPlayerHitsEnemy?.Invoke();

            if (enemyHitsBullet)
            {
                OnEnemyHitsBullet?.Invoke();
                unitView.ForceDestroy();
            }

            _context.AudioService.PlayGlobalFx(_context.AudioService.DestroyClip);
            _context.FxService.PlayAt(FxType.Boom, transform.position, transform.localScale);
        }

        public void ForceDestroy() => OnForceDestroy?.Invoke();
    }
}