using System;
using Factory;
using LoadingSystem.Loading.Operations.Home;
using UnityEngine;

namespace Fx
{
    public class FxService : IFxService
    {
        private readonly IFactory<IFx> _factory;
        private readonly float _duration;

        private FxService(IFactory<IFx> factory, float duration, HomeSceneLoadingContext context)
        {
            _factory = factory;
            _duration = duration;
            context.FxService = this;
        }

        public void PlayAt(FxType type, Vector3 position)
        {
            var fx = _factory.Create();
            fx.PlayFx(type, position, _duration);
        }
        
        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Fx Fx { get; private set; }
            [field: SerializeField] public int FxCount { get; private set; }
            [field: SerializeField] public float Duration { get; private set; }
        }
    }
}