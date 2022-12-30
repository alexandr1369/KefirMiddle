namespace Bullet.LifeTimeChecker
{
    public interface IBulletsLifeTimeChecker
    {
        void Add(Bullet bullet);
        void Remove(Bullet bullet);
    }
}