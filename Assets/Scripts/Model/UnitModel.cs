using System;

namespace Model
{
    public class UnitModel : IUnitModel
    {
        public event Action<int> OnTookDamage;
        public event Action OnDestroyed;
        
        public int Health { get; set; }
        
        public UnitModel(int health = 1) => Health = health;

        public void TakeDamage(int damage = 1)
        {
            Health = Math.Clamp(Health - damage, 0, int.MaxValue);
            OnTookDamage?.Invoke(damage);

            if (Health <= 0) 
                OnDestroyed?.Invoke();
        }
    }
}