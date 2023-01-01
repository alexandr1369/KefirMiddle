using UnityEngine;

namespace Enemy
{
    public interface IEnemyTypeAdapter
    {
        Type EnemyType { get; }
        void SetEnemyType(Type type, Vector3? velocity = null);
        
        public enum Type
        {
            Asteroid,
            BrokenAsteroid,
            Ufo
        }
    }
}