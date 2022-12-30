using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bullet
{
    public interface IBulletsService
    {
        event Action<Bullet> OnSpawned;
        List<Bullet> Bullets { get; }
        BulletsService.Settings ServiceSettings { get; }
        void SpawnBulletAt(Vector3 position, Vector3 direction);
    }
}