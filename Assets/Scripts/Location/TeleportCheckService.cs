using Presenter;
using UnityEngine;
using Utils;

namespace Location
{
    public class TeleportCheckService : ITeleportCheckService
    {
        private readonly ISceneBoundsService _sceneBoundsService;
        
        public TeleportCheckService(ISceneBoundsService sceneBoundsService) => _sceneBoundsService = sceneBoundsService;
        
        public void CheckForTeleport(IUnitPresenter presenter)
        {
            var scale = presenter.LocalScale.x;
            
            if (presenter.Position.x > _sceneBoundsService.Right + scale 
                && IsMovingInDirection(presenter, Vector3.right))
            {
                presenter.Transform.SetX(_sceneBoundsService.Left - scale);
            }
            else if (presenter.Position.x < _sceneBoundsService.Left - scale 
                     && IsMovingInDirection(presenter, -Vector3.right))
            {
                presenter.Transform.SetX(_sceneBoundsService.Right + scale);
            }
            else if (presenter.Position.y < _sceneBoundsService.Bottom - scale 
                     && IsMovingInDirection(presenter, -Vector3.up))
            {
                presenter.Transform.SetY(_sceneBoundsService.Top + scale);
            }
            else if (presenter.Position.y > _sceneBoundsService.Top + scale 
                     && IsMovingInDirection(presenter, Vector3.up))
            {
                presenter.Transform.SetY(_sceneBoundsService.Bottom - scale);
            }
        }
        
        private bool IsMovingInDirection(IUnitPresenter presenter, Vector3 dir) => 
            Vector3.Dot(dir, presenter.Velocity) > 0;
    }
}