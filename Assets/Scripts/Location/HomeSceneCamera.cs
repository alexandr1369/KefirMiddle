using UnityEngine;

namespace Location
{
    public class HomeSceneCamera : MonoBehaviour, IHomeSceneCamera
    {
        [field: SerializeField] public Camera Camera { get; private set; }
    }
}