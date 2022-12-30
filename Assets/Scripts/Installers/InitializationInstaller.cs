using UnityEngine;
using Zenject;

namespace Installers
{
    public class InitializationInstaller : MonoInstaller
    {
        private const int TARGET_FPS = 60;

        public override void InstallBindings() => InitGameData();

        private static void InitGameData() => Application.targetFrameRate = TARGET_FPS;
    }
}