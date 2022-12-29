using Location;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class LocationInstaller : MonoInstaller
    {
        [field: SerializeField] private HomeSceneCamera Camera { get; set; }
        
        public override void InstallBindings()
        {
            BindCamera();
            BindBounds();
        }

        private void BindCamera()
        {
            Container.Bind<IHomeSceneCamera>()
                .To<HomeSceneCamera>()
                .FromInstance(Camera)
                .AsSingle();
        }

        private void BindBounds()
        {
            Container.Bind<ISceneBoundsService>()
                .To<SceneBoundsService>()
                .AsSingle();
        }
    }
}