using System;

namespace Model
{
    public class UnitUnitModel : IUnitModel
    {
        public event Action<int> OnTookDamage;
        public event Action OnDestroyed;
        
        public int Health { get; private set; }
        
        public UnitUnitModel(int health = 0)
        {
            Health = health;
        }

        public void TakeDamage(int damage)
        {
            Health = Math.Clamp(Health - damage, 0, int.MaxValue);
            OnTookDamage?.Invoke(damage);
            
            if(Health <= 0)
                OnDestroyed?.Invoke();
        }
    }
}