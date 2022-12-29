using Model;
using Presenter;
using View;
using Zenject;

namespace Installers
{
    public class UnitSubContainerInstaller : Installer
    {
        private UnitView _unitView;
        
        [Inject]
        private void Construct(GameSettingsInstaller.GameSettings settings) => 
            _unitView = Container.InstantiatePrefabForComponent<UnitView>(settings.UnitViewPrefab);

        public override void InstallBindings()
        {
            BindModel();
            BindView();
            BindPresenter();
        }

        private void BindModel()
        {
            Container.Bind<IModel>()
                .To<UnitModel>()
                .AsSingle();
        }

        private void BindView()
        {
            Container.Bind<UnitView>()
                .FromInstance(_unitView)
                .AsSingle();
            _unitView.transform.parent = null;
        }

        private void BindPresenter()
        {
            Container.Bind<UnitPresenter>()
                .AsSingle();
        }
    }
}