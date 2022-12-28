using Presenter;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UnitPresenter>()
                .FromSubContainerResolve()
                .ByInstaller<UnitSubContainerInstaller>()
                .AsSingle();
            Container.Resolve<UnitPresenter>();
        }
    }
}