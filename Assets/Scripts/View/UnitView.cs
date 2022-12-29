using UnityEngine;

namespace View
{
    public class UnitView : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody Rb { get; private set; }
    }
}