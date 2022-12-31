using Factory;
using Fx;
using UnityEngine;
using Utils.Pool;
using Zenject;

namespace Installers
{
    public class FxInstaller : MonoInstaller
    {
        [field: SerializeField] private FxService.Settings Settings { get; set; }
        
        public override void InstallBindings()
        {
            BindFx();
            BindPool();
            BindFactory();
            BindService();
        }

        private void BindFx()
        {
            Container.Bind(typeof(IFx), typeof(Fx.Fx))
                .To<Fx.Fx>()
                .FromComponentInNewPrefab(Settings.Fx)
                .AsTransient();
        }

        private void BindPool()
        {
            Container.Bind(typeof(IInitializable), typeof(MonoPool<Fx.Fx>))
                .To<Fx.Fx.Pool>()
                .AsSingle()
                .WithArguments(Settings.FxCount);
        }

        private void BindFactory()
        {
            Container.Bind<Factory.IFactory<IFx>>()
                .To<FxFactory>()
                .AsSingle();
        }

        private void BindService()
        {
            Container.BindInterfacesAndSelfTo<FxService>()
                .AsSingle()
                .WithArguments(Settings.Duration);
            Container.Resolve<IFxService>();
        }
    }
}