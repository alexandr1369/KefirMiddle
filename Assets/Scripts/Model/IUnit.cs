using System;

namespace Model
{
    public interface IUnit
    {
        event Action<int> OnTookDamage;
        event Action OnDestroyed;
        int Health { get; }
        void TakeDamage(int damage);
    }
}