using LoadingSystem.Loading.Operations.Home;
using StateSystem;
using StateSystem.UserState;
using UnityEngine;
using Zenject;

namespace Audio
{
    public class AudioService : MonoBehaviour, IAudioService
    {
        [field: SerializeField] public AudioSource MusicAudioSource { get; set; }
        [field: SerializeField] public AudioSource SoundAudioSource { get; set; }

        [field: Header("Clips")]
        [field: SerializeField] public AudioClip HomeMusicClip { get; set; }
        [field: SerializeField] public AudioClip ClickClip { get; set; }
        [field: SerializeField] public AudioClip ShootClip { get; set; }
        [field: SerializeField] public AudioClip DestroyClip { get; set; }
    
        public IGameController GameController { get; set; }
        public IGameSettings GameSettings { get; set; }
        public int PlayedLocalFxCount { get; set; }
        public bool IsMusicEnabled { get; private set; }
        public bool IsSoundEnabled { get; private set; }
    
        [Inject]
        public void Construct(IGameController gameController, HomeSceneLoadingContext context)
        {
            GameController = gameController;
            GameSettings = GameController.State.UserState.GameSettings;
            IsSoundEnabled = GameSettings.IsSoundEnabled;
            IsMusicEnabled = GameSettings.IsMusicEnabled;
            context.AudioService = this;
        }
    
        public void PlayHomeMusic()
        {
            if (MusicAudioSource.clip == HomeMusicClip && MusicAudioSource.isPlaying)
                return;

            MusicAudioSource.clip = HomeMusicClip;

            if (!IsMusicEnabled) 
                return;
        
            MusicAudioSource.Play();
        }

        public void PlayLocalFx(AudioSource source, AudioClip clip)
        {
            if(!IsSoundEnabled)
                return;
        
            source.Stop();
            source.clip = clip;
            source.Play();
        
            PlayedLocalFxCount++;
        }
    
        public void StopLocalFx(AudioSource source) => source.Stop();

        public void PlayGlobalFx(AudioClip clip)
        {
            if(!IsSoundEnabled)
                return;
        
            if(SoundAudioSource.isPlaying)
                SoundAudioSource.Stop();
        
            SoundAudioSource.clip = clip;
            SoundAudioSource.Play();
        }

        public void SetMusicVolume(bool state)
        {
            IsMusicEnabled = state;
            GameSettings.IsMusicEnabled = IsMusicEnabled;
            GameController.Save();

            if(!IsMusicEnabled && MusicAudioSource.isPlaying)
                MusicAudioSource.Stop();
            else if(IsMusicEnabled && MusicAudioSource.clip != null)
                MusicAudioSource.Play();
        }

        public void SetSoundVolume(bool state)
        {
            IsSoundEnabled = state;
            GameSettings.IsSoundEnabled = IsSoundEnabled;
            GameController.Save();
        }

        public void PreloadHomeAudio()
        {
            ShootClip.LoadAudioData();
            DestroyClip.LoadAudioData();
        }
    }
}