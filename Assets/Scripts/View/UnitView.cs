using UnityEngine;

namespace View
{
    public class UnitView : MonoBehaviour, IUnitView
    {
        [field: SerializeField] public Rigidbody Rb { get; private set; }
    }
}