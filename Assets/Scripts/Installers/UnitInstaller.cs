using Presenter;
using Zenject;

namespace Installers
{
    public class UnitInstaller : MonoInstaller
    {
        private GameSettingsInstaller.GameSettings _settings;

        [Inject]
        private void Construct(GameSettingsInstaller.GameSettings settings) => _settings = settings; 
        
        public override void InstallBindings()
        {
            Container.Bind<IUnitPresenter>()
                .To<UnitUnitPresenter>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<UnitSubContainerInstaller>(_settings.UnitViewPrefab)
                .AsTransient();
            Container.Resolve<IUnitPresenter>();
        }
    }
}