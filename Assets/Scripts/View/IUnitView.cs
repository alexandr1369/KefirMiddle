using UnityEngine;

namespace View
{
    public interface IUnitView
    {
        Rigidbody Rb { get; }
        MeshRenderer MeshRenderer { get; }
        MeshFilter MeshFilter { get; }
        Transform BulletsSpawnPoint { get; }
        public bool IsActive { get; set; }
    }
}