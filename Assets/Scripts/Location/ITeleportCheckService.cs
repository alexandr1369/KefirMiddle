using Presenter;

namespace Location
{
    public interface ITeleportCheckService
    {
        void CheckForTeleport(IUnitPresenter presenter);
    }
}