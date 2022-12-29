using Model;
using Presenter;
using Zenject;

namespace Installers
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
                .To<UnitUnitModel>()
                .AsSingle();
        }
        
        private void BindPresenter()
        {
            Container.BindInterfacesAndSelfTo<UnitUnitPresenter>()
                .AsSingle();
        }
    }
}