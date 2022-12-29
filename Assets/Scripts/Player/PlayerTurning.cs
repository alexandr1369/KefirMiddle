using Location;
using UnityEngine;
using Zenject;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Player
{
    public class PlayerTurning : IFixedTickable
    {
        private readonly Player _player;
        private readonly IHomeSceneCamera _camera;

        private PlayerTurning(Player player, IHomeSceneCamera camera)
        {
            _player = player;
            _camera = camera;
        }

        public void FixedTick()
        {
            var mouseRay = _camera.Camera.ScreenPointToRay(Input.mousePosition);
            var mousePosition = mouseRay.origin;
            mousePosition.z = 0;

            var targetDirection = mousePosition - _player.Presenter.Position;
            targetDirection.z = 0;
            targetDirection.Normalize();

            _player.Presenter.Rotation = 
                Quaternion.LookRotation(targetDirection) * Quaternion.AngleAxis(90, Vector3.up);
        }
    }
}