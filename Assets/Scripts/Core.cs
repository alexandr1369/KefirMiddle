using Factory;
using Presenter;
using UnityEngine;

public class Core
{
    public IUnitPresenter Presenter { get; protected set; }
        
    private readonly IFactory<IUnitPresenter> _factory;

    protected Core(IFactory<IUnitPresenter> factory) => _factory = factory;

    public virtual void Init(Transform parent)
    {
        Presenter = _factory.Create();
        Presenter.IsActive = true;
        Presenter.IsPlayer = true;
        Presenter.IsBullet = false;
        Presenter.Parent = parent;
    }
}