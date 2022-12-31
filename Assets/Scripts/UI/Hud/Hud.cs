using LoadingSystem.Loading.Operations.Home;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.Hud
{
    public class Hud : MonoBehaviour
    {
        [field: SerializeField] private float UpdateDelay { get; set; }
        [field: SerializeField] private TextMeshProUGUI PlayerPosition { get; set; }
        [field: SerializeField] private TextMeshProUGUI PlayerRotationAngles { get; set; }
        [field: SerializeField] private TextMeshProUGUI PlayerVelocity { get; set; }
        [field: SerializeField] private TextMeshProUGUI PlayerExtraShootingReloadDelay { get; set; }
        [field: SerializeField] private TextMeshProUGUI PlayerExtraShootingCount { get; set; }
        
        private HomeSceneLoadingContext _context;
        private string _playerPositionTextFormat;
        private string _playerRotationAnglesTextFormat;
        private string _playerVelocityTextFormat;
        private string _playerExtraShootingReloadingDelayTextFormat;
        private string _playerExtraShootingCountTextFormat;
        private float _updateDelay;

        [Inject]
        private void Construct(HomeSceneLoadingContext context) => _context = context;

        private void Awake()
        {
            _playerPositionTextFormat = PlayerPosition.text;
            _playerRotationAnglesTextFormat = PlayerRotationAngles.text;
            _playerVelocityTextFormat = PlayerVelocity.text;
            _playerExtraShootingReloadingDelayTextFormat = PlayerExtraShootingReloadDelay.text;
            _playerExtraShootingCountTextFormat = PlayerExtraShootingCount.text;
        }

        private void Update()
        {
            if(!CanUpdate())
                return;

            PlayerPosition.text = 
                string.Format(_playerPositionTextFormat, _context.PlayerMovementAdapter.Position);
            PlayerRotationAngles.text = 
                string.Format(_playerRotationAnglesTextFormat, _context.PlayerRotationAdapter.Rotation.eulerAngles);
            PlayerVelocity.text = 
                string.Format(_playerVelocityTextFormat, _context.PlayerMovementAdapter.Velocity.magnitude);
            PlayerExtraShootingReloadDelay.text = 
                string.Format(_playerExtraShootingReloadingDelayTextFormat, _context.PlayerExtraShootingAdapter.ReloadDelay);
            PlayerExtraShootingCount.text = 
                string.Format(_playerExtraShootingCountTextFormat, _context.PlayerExtraShootingAdapter.Count);
            
            _updateDelay = UpdateDelay;
        }

        private bool CanUpdate()
        {
            if (_context.PlayerMovementAdapter == null
                || _context.PlayerRotationAdapter == null
                || _context.PlayerExtraShootingAdapter == null)
            {
                return false;
            }

            if (_updateDelay <= 0)
                return true;
            
            _updateDelay -= Time.deltaTime;
            
            return false;
        }
    }
}