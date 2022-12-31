using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Utils.Pool
{
    public abstract class MonoPool<T> : IInitializable where T : MonoBehaviour
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
            var item = Items.Find(item => !item.gameObject.activeSelf);
            Items.Remove(item);
            item.gameObject.SetActive(true);
            
            return item;
        }

        public void Despawn(T item)
        {
            Items.Add(item);
            item.gameObject.SetActive(false);
        }
    }
}