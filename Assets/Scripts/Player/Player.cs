using Presenter;
using UnityEngine;
using Utils;

namespace Player
{
    public class Player
    {
        public IUnitPresenter Presenter { get; protected set; }
        
        private readonly IFactory<IUnitPresenter> _factory;

        protected Player(IFactory<IUnitPresenter> factory) => _factory = factory;

        public virtual void Init(Transform parent)
        {
            Presenter = _factory.Create();
            Presenter.IsActive = true;
            Presenter.IsPlayer = true;
            Presenter.Parent = parent;
        }
    }
}