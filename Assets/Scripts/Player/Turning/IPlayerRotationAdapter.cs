using UnityEngine;

namespace Player.Turning
{
    public interface IPlayerRotationAdapter
    {
        Quaternion Rotation { get; }
    }
}