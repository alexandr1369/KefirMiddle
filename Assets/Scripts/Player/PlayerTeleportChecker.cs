using Location;
using Zenject;

namespace Player
{
    public class PlayerTeleportChecker : ITickable
    {
        private readonly ITeleportCheckService _teleportCheckService;
        private readonly Core _core;

        private PlayerTeleportChecker(Core core, ITeleportCheckService teleportCheckService)
        {
            _teleportCheckService = teleportCheckService;
            _core = core;
        }

        public void Tick() => _teleportCheckService.CheckForTeleport(_core.Presenter);
    }
}