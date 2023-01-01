using System;
using UnityEngine;

namespace View
{
    public interface IUnitView
    {
        event Action OnEnemyHitsBullet;
        event Action OnPlayerHitsEnemy;
        event Action OnForceDestroy;
        Rigidbody Rb { get; }
        MeshRenderer MeshRenderer { get; }
        MeshFilter MeshFilter { get; }
        Transform BulletsSpawnPoint { get; }
        AudioSource AudioSource { get; }
        public bool IsActive { get; set; }
        public bool IsPlayer { get; set; }
        public bool IsBullet { get; set; }
        public void ForceDestroy();
    }
}