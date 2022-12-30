using Movement;

namespace Player.Movement
{
    public interface IPlayerMovement
    {
        Core Core { get; }
        PlayerMovement.Settings MovementSettings { get; }
        void SetMoveBehaviour(IMovable moveBehaviour);
    }
}