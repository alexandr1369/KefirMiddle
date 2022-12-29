using Factory;
using Presenter;
using UnityEngine;
using Utils;
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
            BindPresenter();
            BindPool();
            BindFactory();
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
            Container.Bind(typeof(IInitializable), typeof(Pool<IUnitPresenter>))
                .To<UnitPresenter.Pool>()
                .AsSingle()
                .WithArguments(_settings.UnitViewsCount);
        }

        private void BindFactory()
        {
            Container.Bind<Utils.IFactory<IUnitPresenter>>()
                .To<UnitPresenterFactory>()
                .AsSingle();
        }
    }
}