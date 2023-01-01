namespace Enemy
{
    public interface IEnemyTypeAdapter
    {
        void SetEnemyType(Type type);
        
        public enum Type
        {
            Asteroid,
            Ufo
        }
    }
}