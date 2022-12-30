using System;
using UnityEngine;

namespace View
{
    /// <summary>
    /// Base view for all units.
    /// </summary>
    public class UnitView : MonoBehaviour, IUnitView
    {
        public event Action OnEnemyHitsBullet;
        public event Action OnPlayerHitsEnemy;
        
        [field: SerializeField] public Rigidbody Rb { get; private set; }
        [field: SerializeField] public MeshRenderer MeshRenderer { get; private set; }
        [field: SerializeField] public MeshFilter MeshFilter { get; private set; }
        [field: SerializeField] public Transform BulletsSpawnPoint { get; private set;}

        public bool IsActive
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value); 
        }

        public bool IsPlayer { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            var unitView = other.GetComponent<UnitView>();
            
            if(!unitView)
                return;
            
            InteractWith(unitView);
        }

        private void InteractWith(IUnitView unitView)
        {
            var playerHitsEnemy = IsPlayer && !unitView.IsPlayer;
            // var enemyHitsBullet = !IsPlayer &&
            
            if(playerHitsEnemy)
                OnPlayerHitsEnemy?.Invoke();
        }
    }
}