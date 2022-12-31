using LoadingSystem.Loading.Operations.Home;
using Location;
using UnityEngine;
using Zenject;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Player.Turning
{
    public class PlayerRotation : IFixedTickable, IPlayerRotationAdapter
    {
        public Quaternion Rotation => _core.Presenter.Rotation;
        
        private readonly Core _core;
        private readonly IHomeSceneCamera _camera;

        private PlayerRotation(Core core, IHomeSceneCamera camera, HomeSceneLoadingContext context)
        {
            _core = core;
            _camera = camera;
            context.PlayerRotationAdapter = this;
        }

        public void FixedTick()
        {
            var mouseRay = _camera.Camera.ScreenPointToRay(Input.mousePosition);
            var mousePosition = mouseRay.origin;
            mousePosition.z = 0;

            var targetDirection = mousePosition - _core.Presenter.Position;
            targetDirection.z = 0;
            targetDirection.Normalize();

            _core.Presenter.Rotation = 
                Quaternion.LookRotation(targetDirection) * Quaternion.AngleAxis(90, Vector3.up);
        }
    }
}