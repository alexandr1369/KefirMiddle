using UnityEngine;
using Utils.Pool;
using Zenject;

namespace Fx
{
    public class Fx : MonoBehaviour, IFx
    {
        [field: SerializeField] private ParticleSystem BoomFx { get; set; }

        [Inject]
        private void Construct(MonoPool<Fx> pool) => _pool = pool;

        private float _timeLeftToDespawn;
        private MonoPool<Fx> _pool;

        private void Update()
        {
            if (_timeLeftToDespawn > 0)
            {
                _timeLeftToDespawn -= Time.deltaTime;
                return;
            }
         
            _pool.Despawn(this);
        }

        public void PlayFx(FxType type, Vector3 position, float duration)
        {
            switch (type)
            {
                case FxType.Boom:
                    
                    BoomFx.Play();
                    break;
            }
            
            transform.position = position;
            _timeLeftToDespawn = duration;
        }

        public class Pool : MonoPool<Fx>
        {
        }
    }
}