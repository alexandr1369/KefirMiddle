using Cysharp.Threading.Tasks;
using DialogueSystem;
using Movement;
using Player.Movement;
using UI.Dialogues;
using Zenject;

namespace Player
{
    public class PlayerDeathChecker : IInitializable
    {
        private readonly Core _core;
        private readonly IPlayerMovement _movement;
        private readonly IDialoguesService _dialoguesService;

        private PlayerDeathChecker(
            Core core,
            IPlayerMovement movement,
            IDialoguesService dialoguesService)
        {
            _core = core;
            _movement = movement;
            _dialoguesService = dialoguesService;
        }

        public async void Initialize()
        {
            await UniTask.Yield();
            
            _core.Presenter.OnDestroyed += OnDestroyed;
        }

        private void OnDestroyed()
        {
            _movement.SetMoveBehaviour(new NoMoveBehaviour());
            _dialoguesService.OpenDialogue<DefeatDialogue>();
        }
    }
}