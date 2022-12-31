using Factory;
using Location;
using Presenter;
using Utils;
using Utils.Pool;
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
            BindTeleportCheckService();
            BindPresenter();
            BindPool();
            BindFactory();
        }

        private void BindTeleportCheckService()
        {
            Container.Bind<ITeleportCheckService>()
                .To<TeleportCheckService>()
                .AsSingle();
        }
        
        private void BindPresenter()
        {
            Container.Bind<IUnitPresenter>()
                .To<UnitPresenter>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<UnitSubContainerInstaller>(_settings.UnitViewPrefab)
                .UnderTransform(transform)
                .AsTransient();
        }

        private void BindPool()
        {
            Container.Bind(typeof(IInitializable), typeof(UnitPresenterPool<IUnitPresenter>))
                .To<UnitPresenter.Pool>()
                .AsSingle()
                .WithArguments(_settings.UnitViewsCount);
        }

        private void BindFactory()
        {
            Container.Bind<Factory.IFactory<IUnitPresenter>>()
                .To<UnitPresenterFactory>()
                .AsSingle();
        }
    }
}