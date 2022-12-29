namespace Location
{
    public class SceneBoundsService : ISceneBoundsService
    {
        private readonly IHomeSceneCamera _camera;
        
        private SceneBoundsService(IHomeSceneCamera camera) => _camera = camera;

        public float Bottom => -ExtentHeight;

        public float Top => ExtentHeight;

        public float Left => -ExtentWidth;

        public float Right => ExtentWidth;

        public float ExtentHeight => _camera.Camera.orthographicSize;
        public float Height => ExtentHeight * 2.0f;

        public float ExtentWidth => _camera.Camera.aspect * _camera.Camera.orthographicSize;
        public float Width => ExtentWidth * 2.0f;
    }
}