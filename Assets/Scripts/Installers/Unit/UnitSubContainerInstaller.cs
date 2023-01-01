using Model;
using Presenter;
using Zenject;

namespace Installers.Unit
{
    public class UnitSubContainerInstaller : Installer
    {
        public override void InstallBindings()
        {
            BindModel();
            BindPresenter();
        }

        private void BindModel()
        {
            Container.Bind<IUnitModel>()
                .To<UnitModel>()
                .AsSingle();
        }

        private void BindPresenter()
        {
            Container.BindInterfacesAndSelfTo<UnitPresenter>()
                .AsSingle();
        }
    }
}