using Presenter;
using UnityEngine;
using Zenject;

namespace Bullet
{
    public class Bullet : Core
    {
        private readonly BulletsService.Settings _settings;

        protected Bullet(
            /*[InjectOptional(Id = "Bullets")]*/Utils.IFactory<IUnitPresenter> factory, 
            BulletsService.Settings settings) 
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