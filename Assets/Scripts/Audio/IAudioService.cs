using StateSystem;
using StateSystem.UserState;
using UnityEngine;

namespace Audio
{
    public interface IAudioService
    {
        AudioSource MusicAudioSource { get; }
        AudioSource SoundAudioSource { get; }
        AudioClip HomeMusicClip { get; }
        AudioClip ClickClip { get; }
        AudioClip ShootClip { get; }
        AudioClip DestroyClip { get; }
        IGameController GameController { get; }
        IGameSettings GameSettings { get; }
        int PlayedLocalFxCount { get; }
        bool IsMusicEnabled { get; }
        bool IsSoundEnabled { get; }
        void PlayHomeMusic();
        void PlayLocalFx(AudioSource source, AudioClip clip);
        void StopLocalFx(AudioSource source);
        void PlayGlobalFx(AudioClip clip);
        void SetMusicVolume(bool state);
        void SetSoundVolume(bool state);
        void PreloadHomeAudio();
    }
}