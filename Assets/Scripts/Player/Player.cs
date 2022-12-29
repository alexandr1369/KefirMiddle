using Presenter;
using UnityEngine;
using Utils;

namespace Player
{
    public class Player
    {
        public IUnitPresenter Presenter { get; private set; }
        
        private readonly IFactory<IUnitPresenter> _factory;

        private Player(IFactory<IUnitPresenter> factory)
        {
            _factory = factory;
        }

        public void Init(Transform parent)
        {
            Presenter = _factory.Create();
            Presenter.IsActive = true;
            Presenter.Parent = parent;
        }
    }
}