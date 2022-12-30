using System;
using UnityEngine;

namespace StateSystem.UserState
{
    [Serializable]
    public class GameSettings : IGameSettings
    {
        [field: SerializeField] public bool IsMusicEnabled { get; set; }
        [field: SerializeField] public bool IsSoundEnabled { get; set; }
        [field: SerializeField] public bool IsVibrationEnabled { get; set; }
        
        public GameSettings()
        {
            IsMusicEnabled = true;
            IsSoundEnabled = true;
            IsVibrationEnabled = true;
        }
    }

    public interface IGameSettings
    {
        bool IsMusicEnabled { get; set; }
        bool IsSoundEnabled { get; set; }
        bool IsVibrationEnabled { get; set; }
    }
}