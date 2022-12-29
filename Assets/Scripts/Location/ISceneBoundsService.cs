namespace Location
{
    public interface ISceneBoundsService
    {
        float Bottom { get; }
        float Top { get; }
        public float Left { get; }
        public float Right { get; }
        public float ExtentHeight { get; }
        public float Height { get; }
        public float ExtentWidth { get; }
        public float Width { get; }
    }
}