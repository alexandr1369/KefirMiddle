using System;
using UnityEngine;
using View;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "Кефир/Settings/New Game Settings Installer", fileName = "GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller
    {
        [field: SerializeField] private GameSettings Settings { get; set; }

        public override void InstallBindings()
        {
            Container.Bind<GameSettings>()
                .FromInstance(Settings)
                .AsSingle();
        }

        [Serializable]
        public class GameSettings
        {
            [field: SerializeField] public UnitView UnitViewPrefab { get; private set; }
        }
    }
}