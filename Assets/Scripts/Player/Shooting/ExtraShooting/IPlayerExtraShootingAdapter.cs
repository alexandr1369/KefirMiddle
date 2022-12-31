namespace Player.Shooting.ExtraShooting
{
    public interface IPlayerExtraShootingAdapter
    {
        float ReloadDelay { get; }
        int Count { get; }
    }
}