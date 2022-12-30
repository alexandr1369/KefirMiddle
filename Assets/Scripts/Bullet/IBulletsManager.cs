using System.Collections.Generic;

namespace Bullet
{
    public interface IBulletsManager
    {
        List<Bullet> Bullets { get; }
    }
}