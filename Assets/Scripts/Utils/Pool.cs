using System.Collections.Generic;
using Presenter;
using Zenject;

namespace Utils
{
    public abstract class Pool<T> : IInitializable where T : IUnitPresenter
    {
        private DiContainer _diContainer;
        private int _count;
        
        [Inject]
        private void Construct(DiContainer diContainer, int count)
        {
            _diContainer = diContainer;
            _count = count;
        }

        private List<T> Items { get; set; }

        public void Initialize()
        {
            Items = new List<T>();
            
            for (var i = 0; i < _count; i++)
            {
                var item = _diContainer.Resolve<T>();
                Despawn(item);
            }
        }

        public T Spawn()
        {
            var item = Items[^1];
            Items.Remove(item);
            item.IsActive = true;
            
            return item;
        }

        public void Despawn(T item)
        {
            Items.Add(item);
            item.IsActive = false;
        }
    }
}