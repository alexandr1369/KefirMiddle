using Presenter;
using UnityEngine;

namespace Player
{
    public class Core
    {
        public IUnitPresenter Presenter { get; protected set; }
        
        private readonly Factory.IFactory<IUnitPresenter> _factory;

        protected Core(Factory.IFactory<IUnitPresenter> factory) => _factory = factory;

        public virtual void Init(Transform parent)
        {
            Presenter = _factory.Create();
            Presenter.IsActive = true;
            Presenter.IsPlayer = true;
            Presenter.IsBullet = false;
            Presenter.Parent = parent;
        }
    }
}