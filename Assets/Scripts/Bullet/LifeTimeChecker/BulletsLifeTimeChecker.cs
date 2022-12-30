using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Bullet.LifeTimeChecker
{
    public class BulletsLifeTimeChecker : ITickable, IBulletsLifeTimeChecker
    {
        private readonly List<LifeTimeCheckData> _datasets = new();
        private readonly List<Bullet> _bulletsToBeRemoved = new();
        private readonly IBulletsService _service;
        private readonly BulletsService.Settings _settings;
        private float _currentDelay;

        public BulletsLifeTimeChecker(IBulletsService service, BulletsService.Settings settings)
        {
            _service = service;
            _service.OnSpawned += Add;
            _settings = settings;
        }

        public void Tick()
        {
            return;
            
            var currentDateTime = DateTime.Now;
            
            ContinueDatasetsCheck(currentDateTime);
        }

        private void ContinueDatasetsCheck(DateTime currentDateTime)
        {
            _datasets.ForEach(data =>
            {
                var creationDateTime = data.CreationDateTime;
                var secondsPassed = (currentDateTime - creationDateTime).TotalSeconds;

                if (!(secondsPassed >= _settings.LifeTimeDuration))
                    return;
                
                var bullet = data.Bullet;
                _bulletsToBeRemoved.Add(bullet);
                _service.Bullets.Remove(bullet);
            });
            
            _bulletsToBeRemoved.ForEach(bullet =>
            {
                bullet.Presenter.TakeDamage();
                Remove(bullet);
            });
        }

        public void Add(Bullet bullet)
        {
            if(_datasets.Find(t => t.Bullet == bullet) != null)
                return;
            
            _datasets.Add(new LifeTimeCheckData
            {
                Bullet = bullet, 
                CreationDateTime = DateTime.Now
            });
        }

        public void Remove(Bullet bullet)
        {
            var data = _datasets.Find(data => data.Bullet == bullet);
            
            if(data == null)
                return;
            
            _datasets.Remove(data);
        }

        [Serializable]
        private class LifeTimeCheckData
        {
            [field: SerializeField] public Bullet Bullet { get; set; } 
            [field: SerializeField] public DateTime CreationDateTime { get; set; }
        }
    }
}