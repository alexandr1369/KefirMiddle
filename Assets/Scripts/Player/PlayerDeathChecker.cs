using Cysharp.Threading.Tasks;
using DialogueSystem;
using Movement;
using Movement.Behaviour;
using UI.Dialogues;
using Zenject;

namespace Player
{
    public class PlayerDeathChecker : IInitializable
    {
        private readonly Core _core;
        private readonly ICoreMovement _movement;
        private readonly IDialoguesService _dialoguesService;

        private PlayerDeathChecker(
            Core core,
            ICoreMovement movement,
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