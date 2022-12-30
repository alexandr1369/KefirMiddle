namespace LoadingSystem.Loading
{
    public interface ISceneLoadingListener
    {
        void OnLoadingStarted();
        void OnLoadingCompleted();
    }
}