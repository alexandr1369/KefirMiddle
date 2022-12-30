using System;
using UnityEngine;

namespace Presenter
{
    public interface IUnitPresenter
    {
        event Action OnDestroyed;
        Transform Parent { set; }
        Transform BulletsSpawnPoint { get; }
        Transform Transform { get; }
        MeshRenderer MeshRenderer { get; }
        MeshFilter MeshFilter { get; }
        Vector3 Position { get; set; }
        Vector3 Velocity { get; set; }
        Vector3 LocalScale { get; set; }
        Quaternion Rotation { get; set; }
        Vector3 Direction { get; set; }
        float Drag { set; }
        bool IsActive { set; }
        bool IsPlayer { set; }
        bool IsBullet { set; }
        bool IsDead { get; }
        void TakeDamage();
        void AddForce(Vector3 force);
    }
}