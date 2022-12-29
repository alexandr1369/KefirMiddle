using UnityEngine;

namespace View
{
    public interface IUnitView
    {
        Rigidbody Rb { get; }
        public bool IsActive { get; set; }
    }
}