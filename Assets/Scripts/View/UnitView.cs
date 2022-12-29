using UnityEngine;

namespace View
{
    public class UnitView : MonoBehaviour, IUnitView
    {
        [field: SerializeField] public Rigidbody Rb { get; private set; }
        [field: SerializeField] public Transform BulletsSpawnPoint { get; private set;}

        public GameObject Self => gameObject;

        public bool IsActive
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value); 
        }
    }
}