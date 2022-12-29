using UnityEngine;

namespace View
{
    public interface IUnitView
    {
        Rigidbody Rb { get; }
        Transform BulletsSpawnPoint { get; }
        public bool IsActive { get; set; }
    }
}