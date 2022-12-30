namespace Utils
{
    public interface IInputDelegate
    {
        delegate bool InputRestriction(object target);
        /// <summary>
        /// Проверка на дозволенность инпута для таргетируемого объекта.
        /// </summary>
        /// <param name="obj">Таргер-объект.</param>
        /// <returns>Если хотя бы один делегат за интеракт с объектом - вернуть true.</returns>
        bool HasPermission(object obj);
        void AddRestriction(InputRestriction inputRestriction);
        void RemoveRestriction(InputRestriction inputRestriction);
    }
}