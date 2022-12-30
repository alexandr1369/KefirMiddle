using Location;

namespace LoadingSystem.Loading.Operations.Home
{
    public class HomeSceneLoadingContext
    {
        public SceneLoadingService SceneLoadingService { get; set; }
        // public IAudioService AudioService { get; set; }
        public HomeSceneCamera HomeSceneCamera { get; set; }
    }
}