using System.Collections.Generic;

namespace Enemy
{
    public interface IEnemiesManager
    {
        List<Enemy> Enemies { get; }
        EnemiesManager.Settings ManagerSettings { get; }
    }
}