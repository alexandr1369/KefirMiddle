using UnityEngine;

namespace Player.Movement
{
    public interface IPlayerMovementAdapter
    {
        Vector3 Position { get; }
        Vector3 Velocity { get; }
    }
}