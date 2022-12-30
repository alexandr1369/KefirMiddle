using System;
using UnityEngine;

namespace View
{
    public interface IUnitView
    {
        event Action OnEnemyHitsBullet;
        event Action OnPlayerHitsEnemy;
        Rigidbody Rb { get; }
        MeshRenderer MeshRenderer { get; }
        MeshFilter MeshFilter { get; }
        Transform BulletsSpawnPoint { get; }
        public bool IsActive { get; set; }
        public bool IsPlayer { get; set; }
        public bool IsBullet { get; set; }
    }
}