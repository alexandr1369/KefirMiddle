using Presenter;
using UnityEngine;
using Utils;

namespace Enemy
{
    public class Enemy : Player.Player
    {
        private readonly EnemiesManager.Settings _settings;

        protected Enemy(IFactory<IUnitPresenter> factory, EnemiesManager.Settings settings) 
            : base(factory)
        {
            _settings = settings;
        }

        public override void Init(Transform parent)
        {
            base.Init(parent);
            
            Presenter.MeshRenderer.material = _settings.Material;
            Presenter.MeshFilter.mesh = _settings.Mesh;
        }
    }
}