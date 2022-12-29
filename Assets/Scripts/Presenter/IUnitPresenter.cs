using System;
using UnityEngine;

namespace Presenter
{
    public interface IUnitPresenter
    {
        event Action OnDestroyed;
        Transform Parent { set; }
        Transform BulletsSpawnPoint { get; }
        Vector3 Position { get; }
        Vector3 Velocity { get; }
        Quaternion Rotation { get; set; }
        bool IsActive { set; }
        void TakeDamage(int damage);
        void AddForce(Vector3 force);
    }
}