using UnityEngine;

namespace View
{
    public class UnitView : MonoBehaviour, IUnitView
    {
        [field: SerializeField] public Rigidbody Rb { get; private set; }
        [field: SerializeField] public MeshRenderer MeshRenderer { get; private set; }
        [field: SerializeField] public MeshFilter MeshFilter { get; private set; }
        [field: SerializeField] public Transform BulletsSpawnPoint { get; private set;}

        public bool IsActive
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value); 
        }
    }
}