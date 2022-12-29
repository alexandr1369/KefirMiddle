using System;

namespace Model
{
    public interface IUnitModel
    {
        event Action<int> OnTookDamage;
        event Action OnDestroyed;
        int Health { get; set; }
        void TakeDamage(int damage);
    }
}