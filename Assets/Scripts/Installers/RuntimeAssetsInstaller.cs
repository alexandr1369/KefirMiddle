using UnityEngine;
using Zenject;

namespace Installers
{
    public class RuntimeAssetsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // единственный вариант, где юзать Resources будет ОК по сравнению с Addressables
            
            var dialogs = Resources.Load<DialogueSystem.Dialogues>(DialogueSystem.Dialogues.ResourcesPath());
            
            Container.Bind<DialogueSystem.Dialogues>()
                .FromInstance(dialogs)
                .AsSingle();
        }
    }
}