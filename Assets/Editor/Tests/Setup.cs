using Audio;
using NSubstitute;
using StateSystem;
using StateSystem.UserState;
using UnityEngine;

namespace Editor.Tests
{
    public static class Setup
    {
        public static AudioService AudioService()
        {
            var audioService = new GameObject().AddComponent<AudioService>();
            var gameController = GameController();
            var gameSettings = Substitute.For<IGameSettings>();
            var audioClip = Create.AudioClip();
            var audioSource = Create.AudioSource();
            audioService.GameController = gameController; 
            audioService.GameSettings = gameSettings;
            audioService.MusicAudioSource = audioSource;
            audioService.SoundAudioSource = audioSource;
            audioService.HomeMusicClip = audioClip;
            audioService.ClickClip = audioClip;
            audioService.ShootClip = audioClip;
            audioService.DestroyClip = audioClip;
            
            return audioService;
        }

        public static IAudioService AudioServiceInterface()
        {
            var audioService = Substitute.For<IAudioService>();
            var gameController = GameController();
            var gameSettings = Substitute.For<IGameSettings>();
            var audioClip = Create.AudioClip();
            var audioSource = Create.AudioSource();
            audioService.GameController.Returns(gameController); 
            audioService.GameSettings.Returns(gameSettings);
            audioService.MusicAudioSource.Returns(audioSource);
            audioService.SoundAudioSource.Returns(audioSource);
            audioService.HomeMusicClip.Returns(audioClip);
            audioService.ClickClip.Returns(audioClip);
            audioService.ShootClip.Returns(audioClip);
            audioService.DestroyClip.Returns(audioClip);

            return audioService;
        }

        public static IGameController GameController()
        {
            var gameController = Substitute.For<IGameController>();
            gameController.State.Returns(new GameState());

            return gameController;
        }
    }
}