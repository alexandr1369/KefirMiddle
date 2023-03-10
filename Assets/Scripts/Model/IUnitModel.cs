using System;

namespace Model
{
    public interface IUnitModel
    {
        event Action<int> OnTookDamage;
        event Action OnDestroyed;
        void TakeDamage(int damage = 1);
        int Health { get; }
    }
}