using System.Collections.Generic;
using Audio;
using DialogueSystem;
using LoadingSystem.Loading;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class DontDestroyOnLoadObjectsInstaller : MonoInstaller
    {
        [field: SerializeField] private DialoguesService DialoguesServicePrefab { get; set; }
        [field: SerializeField] private AudioService AudioServicePrefab { get; set; }
        [field: SerializeField] private SceneLoadingService SceneLoadingServicePrefab { get; set; }
        
        private IAudioService _audioService;
        private IDialoguesService _dialoguesService;
        private ISceneLoadingService _sceneLoadingService;
        
        public override void InstallBindings()
        {
            LoadData();
            BindAudioService();
            BindDialoguesService();
            BindSceneLoadingService();
            SetDontDestroyOnLoadObjects();
        }

        private void LoadData()
        {
            _audioService = Container.InstantiatePrefabForComponent<IAudioService>(AudioServicePrefab);
            _dialoguesService = Container.InstantiatePrefabForComponent<IDialoguesService>(DialoguesServicePrefab);
            _sceneLoadingService = Container.InstantiatePrefabForComponent<ISceneLoadingService>(SceneLoadingServicePrefab);
            
            ((Component)_audioService).transform.SetParent(null);
            ((Component)_dialoguesService).transform.SetParent(null);
            ((Component)_sceneLoadingService).transform.SetParent(null);
        }

        private void BindAudioService()
        {
            Container.BindInterfacesAndSelfTo<AudioService>()
                .FromComponentOn(((Component)_audioService).gameObject)
                .AsSingle();
        }

        private void BindDialoguesService()
        {
            Container.Bind<IDialoguesService>()
                .To<DialoguesService>()
                .FromComponentOn(((Component)_dialoguesService).gameObject)
                .AsSingle();
        }

        private void BindSceneLoadingService()
        {
            Container.Bind<ISceneLoadingService>()
                .To<SceneLoadingService>()
                .FromComponentOn(((Component)_sceneLoadingService).gameObject)
                .AsSingle();
        }

        private void SetDontDestroyOnLoadObjects()
        {
            var objects = new List<GameObject>
            {
                ((Component)_audioService).gameObject,
                ((Component)_dialoguesService).gameObject,
                ((Component)_sceneLoadingService).gameObject
            };
            
            objects.ForEach(DontDestroyOnLoad);
        }
    }
}