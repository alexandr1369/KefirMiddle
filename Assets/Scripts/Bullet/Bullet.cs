using Presenter;
using UnityEngine;

namespace Bullet
{
    public class Bullet : Core
    {
        private readonly BulletsManager.Settings _settings;

        protected Bullet(Utils.IFactory<IUnitPresenter> factory, BulletsManager.Settings settings) 
            : base(factory)
        {
            _settings = settings;
        }

        public override void Init(Transform parent)
        {
            base.Init(parent);
            
            Presenter.IsPlayer = false;
            Presenter.IsBullet = true;
            Presenter.MeshRenderer.material = _settings.Material;
            Presenter.MeshFilter.mesh = _settings.Mesh;
        }
    }
}