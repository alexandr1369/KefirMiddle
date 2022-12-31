using LoadingSystem.Loading.Operations.Home;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.Hud
{
    public class Hud : MonoBehaviour
    {
        [field: SerializeField] private float UpdateDelay { get; set; }
        [field: SerializeField] private TextMeshProUGUI PlayerExtraShootingReloadDelay { get; set; }
        [field: SerializeField] private TextMeshProUGUI PlayerExtraShootingCount { get; set; }
        
        private HomeSceneLoadingContext _context;
        private string _playerExtraShootingReloadingDelayTextFormat;
        private string _playerExtraShootingCountTextFormat;
        private float _updateDelay;

        [Inject]
        private void Construct(HomeSceneLoadingContext context) => _context = context;

        private void Awake()
        {
            _playerExtraShootingReloadingDelayTextFormat = PlayerExtraShootingReloadDelay.text;
            _playerExtraShootingCountTextFormat = PlayerExtraShootingCount.text;
        }

        private void Update()
        {
            if(_context.PlayerExtraShootingAdapter == null)
                return;
            
            if (_updateDelay > 0)
            {
                _updateDelay -= Time.deltaTime;
                return;
            }

            PlayerExtraShootingReloadDelay.text = 
                string.Format(_playerExtraShootingReloadingDelayTextFormat, _context.PlayerExtraShootingAdapter.ReloadDelay);
            PlayerExtraShootingCount.text = 
                string.Format(_playerExtraShootingCountTextFormat, _context.PlayerExtraShootingAdapter.Count);
            
            _updateDelay = UpdateDelay;
        }
    }
}