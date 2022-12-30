using StateSystem;
using Utils;

namespace LoadingSystem.Loading
{
    public interface ISceneLoadingService
    {
        static readonly IInputDelegate.InputRestriction ActionsRestriction = _ => false;
        bool LoadingInProgress { get; }
        void ReturnToPreviousLocation();
        void RestartCurrentLocation();
        void ChangeLocation(LocationState location, bool initial = false);
    }
}